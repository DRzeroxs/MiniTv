using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }

        //conductores
        [InverseProperty("genero")]
        public ICollection<SeriesGeneros>? Serie { get; set; }
    }
}
