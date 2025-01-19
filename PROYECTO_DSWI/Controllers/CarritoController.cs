using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTO_DSWI.Controllers;
using PROYECTO_DSWI.Models;

namespace PROYECTO_DSWI.Controllers
{
    public class CarritoController : Controller
    {
        private static Usuario oUsuario;
        PROYECTO_DSWIEntities1 db = new PROYECTO_DSWIEntities1();
        
        private int indice(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.IdProducto == id)
                    return i;
            }
            return -1;
        }
        public ActionResult AgregarCarrito()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AgregarCarrito(int id, int cantidad, int talla)
        {

            if (Session["carrito"] == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(db.PRODUCTO.Find(id), cantidad, talla));
                Session["carrito"] = compras;
            }
            else
            {
                List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
                int posicion = indice(id);
                if (posicion == -1)
                    compras.Add(new CarritoItem(db.PRODUCTO.Find(id), cantidad, talla));
                else
                    compras[posicion].Cantidad+=cantidad;
                Session["carrito"] = compras;
            }
            return Json(new {response = true}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            compras.RemoveAt(indice(id));
            return View("AgregarCarrito");
        }
        public ActionResult FinalizarCompra()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Acceso");
            else
                oUsuario = (Usuario)Session["Usuario"];

            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            if (compras != null && compras.Count > 0)
            {

                COMPRA nuevaCompra = new COMPRA();
                nuevaCompra.IdUsuario = oUsuario.idUsuario;
                nuevaCompra.Total = compras.Sum(x => x.Producto.Precio * x.Cantidad);
                nuevaCompra.FechaCompra = DateTime.Now;

                nuevaCompra.DETALLE_COMPRA = (from producto in compras
                                              select new DETALLE_COMPRA
                                              {
                                                  IdProducto = producto.Producto.IdProducto,
                                                  IdTalla = producto.Talla,
                                                  Cantidad = producto.Cantidad,
                                                  Total = producto.Cantidad * producto.Producto.Precio
                                              }).ToList();
                db.COMPRA.Add(nuevaCompra);
                db.SaveChanges();
                Session["carrito"] = new List<CarritoItem>();
            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}