﻿using Microsoft.AspNetCore.Mvc;

namespace BlocoDeNotas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
