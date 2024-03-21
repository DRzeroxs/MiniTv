using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Productora
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Name { get; set; }

        //conductores
        [InverseProperty("Productora")]
        public ICollection<Serie>? Series { get; set; }
    }
}
