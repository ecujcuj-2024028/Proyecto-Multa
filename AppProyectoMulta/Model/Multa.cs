using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProyectoMulta.Model
{
    public class Multa
    {
        public int IdMulta { get; set; }
        public DateTime FechaMulta { get; set; }
        public DateTime FechaLimite { get; set; }
        public decimal Importe { get; set; }
        public string Lugar { get; set; }
        public string Estado { get; set; }
        public int IdVehiculo { get; set; }
        public int IdAgente { get; set; }
        public int IdTipoMulta { get; set; }
    }
}