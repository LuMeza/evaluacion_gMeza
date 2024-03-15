using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Tickets
    {
        public string Id_Tienda { get; set; }
        public string Id_Registradora { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaHora_Creacion { get; set; }
        public int Ticket { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }
}
