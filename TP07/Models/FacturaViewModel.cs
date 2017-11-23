using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP07.Models
{
    public class FacturaViewModel
    {
        public FacturasModels factura { get; set; }
        public FacturaDetallesModels detalle { get; set; }
        public List<SelectListItem> articulos = new List<SelectListItem>();
    }
}