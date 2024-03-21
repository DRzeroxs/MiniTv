using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Serie
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage ="Maximo 100 caracteres")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Link { get; set; }
        public string? Img { get; set; }

        //Llaves foraneas
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [ForeignKey("Productora")]
        public int ProductoraId { get; set; }
        public Productora Productora { get; set; }

        //conductores
        [InverseProperty("serie")]
        public List<SeriesGeneros> Genero { get; set; }
    }
}
