using Application.Services;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiniTv.Controllers
{
    public class ProductoraController : Controller
    {

        private readonly Context _context;
        private readonly Repository _repository;

        public ProductoraController(Context context, Repository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> PListado()
        {
            return View(await _context.Productoras.ToListAsync());
        }

        [HttpGet]
        public IActionResult AgregarProductora()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarProductora(Productora productora)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(productora);
                return RedirectToAction(nameof(PListado));
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditarProductora(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productora = _context.Productoras.Find(id);

            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProductora(Productora productora)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(productora);
                return RedirectToAction(nameof(PListado));
            }

            return View();
        }

        [HttpGet]
        public IActionResult BorrarProductora(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productora = _context.Productoras.Find(id);

            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        [HttpPost, ActionName("BorrarProductora")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarP(int? id)
        {
            var productora = await _context.Productoras.FindAsync(id);

            if (productora == null)
            {
                return View();
            }

            //borrado
            await _repository.DeleteAsync(productora);

            return RedirectToAction(nameof(PListado));
        }
    }
}
