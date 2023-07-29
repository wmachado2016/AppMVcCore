using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WSM.WebApp.MVC.Services;

namespace WSM.WebApp.MVC.Controllers
{
    public class ContaOldController : Controller
    {
        private readonly IComunicacaoService _autenticacaoService;

        public ContaOldController(IComunicacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        public async Task<ActionResult> GetAll()
        {
            var resposta = await _autenticacaoService.ObterTodas();
            return View("index", resposta);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var resposta = await _autenticacaoService.ObterPorId(id);
            return View(resposta);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(GetAll));
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Edit(int id)
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(GetAll));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(GetAll));
            }
            catch
            {
                return View();
            }
        }
    }
}
