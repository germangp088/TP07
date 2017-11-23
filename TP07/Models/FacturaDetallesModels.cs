using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP07.Models
{
    public class FacturaDetallesModels
    {
        public int id { get; set; }
        public FacturasModels factura { get; set; }
        public int facturaid { get; set; }
        public ArticulosModels articulo { get; set; }
        public int articuloid { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
    }
}