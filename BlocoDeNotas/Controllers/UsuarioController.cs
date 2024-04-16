using Microsoft.AspNetCore.Mvc;

namespace BlocoDeNotas.Controllers
{
    public class UsuarioController : Controller
    {
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
    }
}
