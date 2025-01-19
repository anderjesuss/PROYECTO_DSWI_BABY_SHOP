using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DSWI.Models
{
    public class CarritoItem
    {
        private PRODUCTO _producto;
        public PRODUCTO Producto { get { return _producto; } set { _producto = value; } }
        public int _cantidad;
        public int Cantidad { get { return _cantidad; } set {  _cantidad = value; } }
        public int _talla;
        public int Talla { get { return _talla; } set { _talla = value; } }
        public CarritoItem() { }
        public CarritoItem(PRODUCTO producto, int cantidad, int talla)
        {
            this._producto = producto;
            this._cantidad = cantidad;
            this._talla = talla;
        }
    }
}