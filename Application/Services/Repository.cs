using Application.Interfaces;
using Database;
using Database.Models;
using Database.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class Repository : IRepository
    {
        private readonly Context _context;
        public Repository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(object entity)
        {
            if (entity is Genero genero)
            {
                _context.Generos.Add(genero);
            }

            if (entity is Productora productora)
            {
                _context.Productoras.Add(productora);
            }

            if (entity is SeriesGeneros seriesGeneros)
            {
                _context.SeriesGeneros.Add(seriesGeneros);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object entity)
        {
            if (entity is Genero genero)
            {
                _context.Generos.Remove(genero);
            }

            if (entity is Productora productora)
            {
                _context.Productoras.Remove(productora);
            }

            if (entity is Serie serie)
            {
                _context.Series.Remove(serie);
            }

            if (entity is SeriesGeneros seriesGeneros)
            {
                _context.SeriesGeneros.Remove(seriesGeneros);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(object entity)
        {
            if (entity is Genero genero)
            {
                _context.Generos.Update(genero);
            }

            if (entity is Productora productora)
            {
                _context.Productoras.Update(productora);
            }

            if (entity is SeriesGeneros seriesGeneros)
            {
                _context.SeriesGeneros.Update(seriesGeneros);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddSerieAsync(SerieViewModel viewModel)
        {
            var serie = new Serie
            {
                Name = viewModel.Name,
                Link = viewModel.Link,
                Img = viewModel.Img,
                ProductoraId = viewModel.ProductoraId
            };

            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            var serieId = serie.Id;

            foreach (var generoId in viewModel.GeneroIds)
            {
                _context.SeriesGeneros.Add(new SeriesGeneros { SerieId = serieId, GeneroId = generoId });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync(SerieViewModel viewModel)
        {
            // Agregar toda la informacion necesaria
            var serie = await _context.Series
                .Include(s => s.Genero)
                .FirstOrDefaultAsync(s => s.Id == viewModel.Id);

            if (serie == null)
            {
                return;
            }

            // reemplazamos con los datos tomados del formulario
            serie.Name = viewModel.Name;
            serie.Link = viewModel.Link;
            serie.Img = viewModel.Img;
            serie.ProductoraId = viewModel.ProductoraId;

            // Actualizamos las relaciones muchos a muchos en la tabla intermedia
            var existingGeneroIds = serie.Genero.Select(sg => sg.GeneroId).ToList();
            var newGeneroIds = viewModel.GeneroIds;

            // Eliminamos las relaciones antiguas
            foreach (var existingId in existingGeneroIds)
            {
                if (!newGeneroIds.Contains(existingId))
                {
                    var toRemove = serie.Genero.Single(sg => sg.GeneroId == existingId);
                    _context.SeriesGeneros.Remove(toRemove);
                }
            }

            // Añadimos las nuevas relaciones
            foreach (var newId in newGeneroIds)
            {
                if (!existingGeneroIds.Contains(newId))
                {
                    serie.Genero.Add(new SeriesGeneros { SerieId = serie.Id, GeneroId = newId });
                }
            }

            _context.Series.Update(serie);

            await _context.SaveChangesAsync();
        }
    }
}
