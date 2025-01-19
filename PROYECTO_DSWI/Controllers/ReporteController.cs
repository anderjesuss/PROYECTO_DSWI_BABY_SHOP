using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROYECTO_DSWI.Datos;
using PROYECTO_DSWI.Models;

namespace PROYECTO_DSWI.Controllers
{
    public class ReporteController : Controller
    {
        private static Usuario oUsuario;

        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Acceso");
            else
                oUsuario = (Usuario)Session["Usuario"];

            return View();
        }

        [HttpGet]
        public JsonResult ReporteVentas()
        {
            Reportes objReportes = new Reportes();

            List<ReporteVenta> lista = objReportes.ReporteVentas();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReporteProductos()
        {
            Reportes objReportes = new Reportes();

            List<ReporteProducto> lista = objReportes.ReporteProductos();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}