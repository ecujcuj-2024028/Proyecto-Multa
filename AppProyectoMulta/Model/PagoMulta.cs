using System;

namespace AppProyectoMulta.Model
{
    public class PagoMulta
    {
        public int IdPago { get; set; }
        public int IdMulta { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public decimal MontoPagado { get; set; }
    }
}
