using Database;
using Database.Models;
using Application.Services;
using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiniTv.Controllers
{
    public class GeneroController : Controller
    {
        private readonly Context _context;
        private readonly Repository _repository;

        public GeneroController(Context context, Repository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GListado()
        {
            return View(await _context.Generos.ToListAsync());
        }

        [HttpGet]
        public IActionResult AgregarGenero()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarGenero(Genero genero)
        {
            if(ModelState.IsValid)
            {
                await _repository.AddAsync(genero);
                return RedirectToAction(nameof(GListado));
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditarGenero( int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var genero = _context.Generos.Find(id);

            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarGenero(Genero genero)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(genero);
                return RedirectToAction(nameof(GListado));
            }

            return View();
        }

        [HttpGet]
        public IActionResult BorrarGenero(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = _context.Generos.Find(id);

            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost, ActionName("BorrarGenero")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarG(int? id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if(genero == null)
            {
                return View();
            }

            //borrado
            await _repository.DeleteAsync(genero);

            return RedirectToAction(nameof(GListado));
        }
    }
}
