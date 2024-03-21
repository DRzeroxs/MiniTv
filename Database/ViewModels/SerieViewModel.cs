using Database.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.ViewModels
{
    public class SerieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Link { get; set; }
        public string? Img { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int ProductoraId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public List<int> GeneroIds { get; set; }
        public List<Productora>? Productoras {  get; set; }
        public List<Genero>? Generos {  get; set; } 
    }
}
