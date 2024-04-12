using Microsoft.AspNetCore.Mvc;

namespace BlocoDeNotas.Controllers
{
    public class NotasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
