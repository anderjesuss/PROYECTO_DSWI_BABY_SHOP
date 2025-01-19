using PROYECTO_DSWI.Logica;
using PROYECTO_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROYECTO_DSWI.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string NCorreo, string NClave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = UsuarioLogica.Instancia.Obtener(NCorreo, NClave);

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }

            FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);
            Session["Usuario"] = oUsuario;

            if (oUsuario.EsAdministrador == true)
            {
                return RedirectToAction("IndexProducto", "Administrar");
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }
        }
        public ActionResult Registrarse()
        {
            return View(new Usuario() { NomUsuario = "", ApeUsuario = "", Correo = "", Clave = "", ConfirmarClave = "" });
        }
        [HttpPost]
        public ActionResult Registrarse(string NNombres, string NApellidos, string NCorreo, string NClave, string NConfirmarClave)
        {

            Usuario oUsuario = new Usuario()
            {
                NomUsuario = NNombres,
                ApeUsuario = NApellidos,
                Correo = NCorreo,
                Clave = NClave,
                ConfirmarClave = NConfirmarClave,
                EsAdministrador = false
            };

            if (NClave != NConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View(oUsuario);
            }
            else
            {
                int respuesta = UsuarioLogica.Instancia.Registrar(oUsuario);

                if (respuesta == 0)
                {
                    ViewBag.Error = "Correo ya existente";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Acceso");
                }
            }
        }
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            Session["carrito"] = new List<CarritoItem>();
            return RedirectToAction("Index", "Acceso");
        }
    }
}