using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;

namespace BlocoDeNotas.Controllers
{
    public class NotasController : Controller
    {
        private readonly INotasRepositorio _notaRepositorio;
        public NotasController(INotasRepositorio notasRepositorio)
        {
            _notaRepositorio = notasRepositorio;
        }
        public IActionResult Index()
        {
            List<NotasModel> notas = _notaRepositorio.ListarNotas();
            return View(notas);
        }
        public IActionResult CriarNota()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarNota(NotasModel nota)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notaRepositorio.Adicionar(nota);
                    TempData["mensagemSucesso"] = "Nota salva com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(nota);
            }
            catch (System.Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível salvar a nota. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(NotasModel nota)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notaRepositorio.Editar(nota);
                    TempData["mensagemSucesso"] = "Nota editada com sucesso.";
                    return RedirectToAction("Index");
                }

                return View("Editar", nota);
            }
            catch (System.Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível editar a nota. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
