using PROYECTO_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_DSWI.Controllers
{
    public class TiendaController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static Usuario oUsuario;
        private PROYECTO_DSWIEntities1 db = new PROYECTO_DSWIEntities1();
        IEnumerable<Talla> Tallas()
        {
            List<Talla> temporal = new List<Talla>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_lista_tallas", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Talla t = new Talla()
                {
                    idTalla = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                };
                temporal.Add(t);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Acceso");
            else
                oUsuario = (Usuario)Session["Usuario"];

            ViewBag.tallas = new SelectList(Tallas(), "idTalla", "descripcion");
            return View(db.PRODUCTO.ToList().OrderBy(x => x.Nombre));
        }
    }
}