using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProyectoMulta.Model
{
    public class Agente
    {
        public int IdAgente {  get; set; }
        public string NombreAgente { get; set; }
        public string Rango {  get; set; }
        public double Salario {  get; set; }
        public int IdMunicipio {  get; set; }

    }
}
