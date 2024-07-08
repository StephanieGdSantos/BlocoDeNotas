using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;
using BlocoDeNotas.Data;
using Microsoft.AspNetCore.Session;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlocoDeNotas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly BancoContext _bancoContext;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, BancoContext bancoContext)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                ViewBag.UsuarioID = usuarioID;
                ViewBag.UsuarioNome = usuarioNome;
            }
            else
            {
                return View();
            }

            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult DadosPessoais()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");
            string usuarioPrimeiroNome = usuarioNome.Split(" ")[0];

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                ViewBag.UsuarioID = usuarioID;
                ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
                UsuarioModel Usuario = _usuarioRepositorio.Selecionar(int.Parse(usuarioID));
                return View(Usuario);
            }
            return RedirectToAction("Index", "Usuario");
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Excluir(id);
                    HttpContext.Session.Clear();
                    TempData["mensagemSucesso"] = "Conta excluída com sucesso!";
                    return Logout();
                }

            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível excluir a conta. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.CriarConta(usuario);
                    TempData["mensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch(System.Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível concluir o cadastro. Para mais detalhes: " + erro.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioModel usuario)
        {
            var validacao = _bancoContext.Usuario.Where(a => a.Email.Equals(usuario.Email) && a.Senha.Equals(usuario.Senha)).FirstOrDefault();
            if (validacao != null)
            {
                HttpContext.Session.SetString("UsuarioID", validacao.Id.ToString());
                HttpContext.Session.SetString("UsuarioNome", validacao.Nome);
                HttpContext.Session.SetString("UsuarioPrimeiroNome", (validacao.Nome).Split(" ")[0]);
                return RedirectToAction("Index", "Notas");
            }
            TempData["MensagemErro"] = "Login ou senha incorretos.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Editar(usuario);
                    TempData["mensagemSucesso"] = "Dados editados com sucesso.";
                }
                else
                {
                    TempData["mensagemErro"] = "Dados inválidos.";
                }
            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível editar os dados. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
            }
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");
            string usuarioPrimeiroNome = usuarioNome.Split(" ")[0];
            ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
            ViewBag.UsuarioID = usuarioID;
            return RedirectToAction("Index");
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
