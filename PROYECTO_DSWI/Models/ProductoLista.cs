using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DSWI.Models
{
    public class ProductoLista
    {
        public int Codigo { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}