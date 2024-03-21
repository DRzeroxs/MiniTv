using Microsoft.AspNetCore.Mvc;
using Database.Models;
using Database.ViewModels;
using Database;
using Microsoft.EntityFrameworkCore;
using Application.Services;

namespace MiniTv.Controllers
{
    public class SeriesController : Controller
    {
        private readonly Context _context;
        private readonly Repository _repository;

        public SeriesController(Context context, Repository repository)
        {
            _context = context;
            _repository = repository;
        }

        public IActionResult SListado(string buscado)
        {
            var series = _context.Series
                .Include(s => s.Productora)
                .Include(s => s.Genero)
                    .ThenInclude(sg => sg.genero)
                .ToList();

            ViewBag.Productoras = _context.Productoras.ToList();
            ViewBag.Generos = _context.Generos.ToList();

            if (!String.IsNullOrEmpty(buscado))
            {
                series = series.Where(s => s.Name.Contains(buscado)).ToList(); // Filtrar por nombre de serie
            }

            return View(series);
        }

        [HttpGet]
        public IActionResult Reproducir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = _context.Series.Find(id);

            if (serie == null)
            {
                return NotFound();
            }

            ViewData["Name"] = serie.Name;

            return View(serie);
            
        }

        [HttpGet]
        public IActionResult AgregarSerie()
        {
            var viewModel = new SerieViewModel
            {
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarSerie(SerieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await  _repository.AddSerieAsync(viewModel);

                return RedirectToAction(nameof(SListado));
            } 

            return RedirectToAction(nameof(SListado));
        }

        [HttpGet]
        public IActionResult EditarSerie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = _context.Series.Find(id);

            if (serie == null)
            {
                return NotFound();
            }

            var viewModel = new SerieViewModel
            {
                Name = serie.Name,
                Link = serie.Link,
                Img = serie.Img,
                ProductoraId = serie.ProductoraId,
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarSerie (SerieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateSerieAsync(viewModel);

                return RedirectToAction(nameof(SListado));
            }

            return RedirectToAction(nameof(SListado));
        }

        [HttpGet]
        public IActionResult BorrarSerie(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var serie = _context.Series.Find(id);

            if (serie == null)
            {
                return NotFound();  
            }

            var viewModel = new SerieViewModel
            {
                Name = serie.Name,
                Link = serie.Link,
                Img = serie.Img,
                ProductoraId = serie.ProductoraId,
                Productoras = _context.Productoras.ToList(),
                Generos = _context.Generos.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("BorrarSerie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarS(int? id)
        {
            var serie = _context.Series.Find(id);

            if(serie == null)
            {
                return View();
            }

            await _repository.DeleteAsync(serie);

            return RedirectToAction(nameof(SListado));
        }

        #region "Filtros"

        public IActionResult FiltrarPorProductora(int? productora)
        {
            var series = _context.Series.ToList(); // Obtener todas las series

            if (productora != null)
            {
                series = series.Where(s => s.ProductoraId == productora).ToList(); // Filtrar por productora
            }

            ViewBag.Productoras = _context.Productoras.ToList();

            ViewBag.Productoras = _context.Productoras.ToList();
            ViewBag.Generos = _context.Generos.ToList();

            return View("Slistado", series);
        }

        public IActionResult FiltrarPorGenero(int? genero)
        {
            var series = _context.Series.Include(s => s.Genero).ToList(); // Obtener todas las series con sus géneros asociados

            if (genero != null)
            {
                series = series.Where(s => s.Genero.Any(g => g.GeneroId == genero)).ToList();
            }

            ViewBag.Generos = _context.Generos.ToList();

            ViewBag.Productoras = _context.Productoras.ToList();
            ViewBag.Generos = _context.Generos.ToList();

            return View("Slistado", series);
        }

        #endregion
    }
}