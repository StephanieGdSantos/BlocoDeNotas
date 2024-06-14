using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;
using System.Diagnostics.CodeAnalysis;

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
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                string usuarioPrimeiroNome = usuarioNome.Split(' ')[0];

                ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
                List<NotasModel> notas = _notaRepositorio.ListarNotas(int.Parse(usuarioID));
                return View(notas);
            }
            return RedirectToAction("Usuario", "Index");
        }
        public IActionResult CriarNota()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                string usuarioPrimeiroNome = usuarioNome.Split(' ')[0];
                ViewBag.UsuarioID = usuarioID;
                ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
                return View();
            }
            return RedirectToAction("Usuario", "Index");
        }
        public IActionResult Excluir(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notaRepositorio.Excluir(id);
                    TempData["mensagemSucesso"] = "Nota excluída com sucesso.";
                }

            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível excluir a nota. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
            }
            return RedirectToAction("Index");
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
                else
                {
                    return RedirectToAction("Index", "Usuario");
                }
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
                }
                else
                {
                    TempData["mensagemErro"] = "Dados inválidos.";
                }
            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = "Ops! Não foi possível editar a nota. Tente novamente! " +
                    $"Detalhe do erro: {erro.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Carregar(int id)
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                string usuarioPrimeiroNome = usuarioNome.Split(' ')[0];
                ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
                var nota = _notaRepositorio.Selecionar(id);
                if (nota == null)
                {
                    TempData["mensagemErro"] = "Nota não encontrada.";
                    return RedirectToAction("Index");
                }

                ViewData["SelectedNota"] = nota;
                var notas = _notaRepositorio.ListarNotas(int.Parse(usuarioID));
                return View("Index", notas);
            }
            return RedirectToAction("Usuario", "Index");
        }
    }
}
