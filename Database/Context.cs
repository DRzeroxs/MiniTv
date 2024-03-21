using Database.Models;
using Database.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<SeriesGeneros> SeriesGeneros { get; set; }

        #region "ViewModels"


        #endregion

    }
}
