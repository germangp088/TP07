using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP07.Models
{
    public class FacturasModels
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string numeroFactura { get; set; }
        public List<FacturaDetallesModels> detalles = new List<FacturaDetallesModels>();
        public double total { get; set; }
    }
}