using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;

namespace BlocoDeNotas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult DadosPessoais()
        {
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
    }
}
