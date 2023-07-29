using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WSM.WebApp.MVC.Models;
using WSM.WebApp.MVC.Services;

namespace WSM.WebApp.MVC.Controllers
{
    public class ContaController : Controller
    {
        private readonly IComunicacaoService _autenticacaoService;

        public ContaController(IComunicacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var resposta = await _autenticacaoService.ObterTodas();
            return View(resposta);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var resposta = await _autenticacaoService.ObterPorId(id);
            if (id == null || resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }

        public IActionResult Create()
        {
            return View();
        }
             
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] ContaViewModel contaViewModel)
        {
            if (ModelState.IsValid)
            {
                contaViewModel.Id = Guid.NewGuid();
                await _autenticacaoService.AdicionarConta(contaViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(contaViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var resposta = await _autenticacaoService.ObterPorId(id);
            if (id == null || resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao")] ContaViewModel contaViewModel)
        {
            if (id != contaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _autenticacaoService.AtualizarConta(contaViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(contaViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var resposta = await _autenticacaoService.ObterPorId(id);
            if (id == null || resposta == null)
            {
                return NotFound();
            }             

            return View(resposta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return Problem("Conta não exite");
            }
            var contaViewModel = await _autenticacaoService.ObterPorId(id);
            if (contaViewModel != null)
            {
                await _autenticacaoService.RemoverConta(contaViewModel);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
