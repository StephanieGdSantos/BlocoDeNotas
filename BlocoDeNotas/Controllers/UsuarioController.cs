using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;
using BlocoDeNotas.Data;
using Microsoft.AspNetCore.Session;

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

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                ViewBag.UsuarioID = usuarioID;
                ViewBag.UsuarioNome = usuarioNome;
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }

            return View();
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
                    return RedirectToAction("Index", "Notas");
                }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
