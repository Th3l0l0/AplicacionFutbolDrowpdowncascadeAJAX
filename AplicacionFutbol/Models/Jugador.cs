using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionFutbol.Models
{
    public class Jugador
    {
        public int ide_jug { get; set; }
        public string nom_jug { get; set; }
        public DateTime fna_jug { get; set; }
        public int ide_pais { get; set; }
        public string nom_pais { get; set; }
        public int ide_con { get; set; }
        public string nom_con { get; set; }
        public decimal pes_jug { get; set; }
    }
}