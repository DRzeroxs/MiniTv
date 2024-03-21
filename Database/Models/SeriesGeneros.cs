using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class SeriesGeneros
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("serie")]
        public int SerieId { get; set; }

        [ForeignKey("genero")]
        public int GeneroId { get; set; }

        //conductores
        public Serie serie { get; set; }
        public Genero genero { get; set; }
        


    }
}
