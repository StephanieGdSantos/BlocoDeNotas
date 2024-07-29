using Microsoft.AspNetCore.Mvc;
using BlocoDeNotas.Models;
using BlocoDeNotas.Repositorio;
using BlocoDeNotas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlocoDeNotas.Controllers
{
    public class NotasController : Controller
    {
        private readonly INotasRepositorio _notaRepositorio;
        private readonly BancoContext _bancoContext;
        public NotasController(INotasRepositorio notasRepositorio, BancoContext bancoContext)
        {
            _notaRepositorio = notasRepositorio;
            _bancoContext = bancoContext;
        }
        public async Task<IActionResult> Index()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");

            Console.Write("index");
            try
            {
                Console.Write("entrando");
                if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
                {
                    string usuarioPrimeiroNome = usuarioNome.Split(' ')[0];
                    ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;

                    var notas = await _notaRepositorio.ListarNotas(int.Parse(usuarioID));
                    return View(notas);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops! Falha: " + erro.Message;
                return RedirectToAction("Index", "Usuario");
            }
            return RedirectToAction("Index", "Usuario");
            
        }
        public async Task<IActionResult> CriarNota()
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
            return RedirectToAction("Index", "Usuario");
        }

        //[HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool excluido = await _notaRepositorio.Excluir(id);
                    if (excluido)
                    {
                        TempData["mensagemSucesso"] = "Nota excluída com sucesso.";
                    }
                    else
                    {
                        TempData["mensagemErro"] = "Ops! Não foi possível excluir a nota. Tente novamente!";
                    }
                    
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
        public async Task<IActionResult> CriarNota(NotasModel nota)
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
                    return RedirectToAction("Index");
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
        public async Task<IActionResult> Editar(NotasModel nota)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _notaRepositorio.Editar(nota);
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

        public async Task<IActionResult> Carregar(int id)
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            string usuarioNome = HttpContext.Session.GetString("UsuarioNome");

            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(usuarioNome))
            {
                string usuarioPrimeiroNome = usuarioNome.Split(' ')[0];
                ViewBag.UsuarioPrimeiroNome = usuarioPrimeiroNome;
                var nota = await _notaRepositorio.Selecionar(id);
                if (nota == null)
                {
                    TempData["mensagemErro"] = "Nota não encontrada.";
                    return RedirectToAction("Index");
                }

                ViewData["SelectedNota"] = nota;
                var notas = await _notaRepositorio.ListarNotas(int.Parse(usuarioID));
                return View("Index", notas);
            }
            return RedirectToAction("Index", "Usuario");
        }
    }
}
