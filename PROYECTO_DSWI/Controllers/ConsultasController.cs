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
    public class ConsultasController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        // GET: Consultas

        IEnumerable<UsuarioLista> Usuarios()
        {
            List<UsuarioLista> temporal = new List<UsuarioLista>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec usp_lista_usuarios", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UsuarioLista u = new UsuarioLista()
                {
                    Codigo = dr.GetInt32(0),
                    Nombres = dr.GetString(1),
                    Correo = dr.GetString(2),
                    Administrador = dr.GetBoolean(3)
                };
                temporal.Add(u);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<ProductoLista> Productos()
        {
            List<ProductoLista> temporal = new List<ProductoLista>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec usp_listarProductos", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductoLista p = new ProductoLista()
                {
                    Codigo = dr.GetInt32(0),
                    Producto = dr.GetString(1),
                    Marca = dr.GetString(2),
                    Categoria = dr.GetString(3),
                    Precio = dr.GetDecimal(4),
                    Stock = dr.GetInt32(5)
                };
                temporal.Add(p);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<CategoriaCombo> Categorias()
        {
            List<CategoriaCombo> temporal = new List<CategoriaCombo>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_lista_categoria", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CategoriaCombo c = new CategoriaCombo()
                {
                    idCodigo = dr.GetInt32(0),
                    Descripcion = dr.GetString(1),
                };
                temporal.Add(c);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<UsuarioLista> UsuariosNombre(string nombre)
        {
            List<UsuarioLista> temporal = new List<UsuarioLista>();
            if (string.IsNullOrEmpty(nombre)) return temporal;

            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec usp_consultar_cliente @nombres", cn);
            cmd.Parameters.AddWithValue("@nombres", nombre);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UsuarioLista u = new UsuarioLista()
                {
                    Codigo = dr.GetInt32(0),
                    Nombres = dr.GetString(1),
                    Correo = dr.GetString(2),
                    Administrador = dr.GetBoolean(3)
                };
                temporal.Add(u);
            }
            dr.Close(); cn.Close();
            return temporal;
        }
        IEnumerable<ProductoLista> ProductoCategoria(int idCodigo)
        {
            List<ProductoLista> temporal = new List<ProductoLista>();

            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec usp_consultar_producto @categoria", cn);
            cmd.Parameters.AddWithValue("@categoria", idCodigo);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductoLista p = new ProductoLista()
                {
                    Codigo = dr.GetInt32(0),
                    Producto = dr.GetString(1),
                    Marca = dr.GetString(2),
                    Categoria = dr.GetString(3),
                    Precio = dr.GetDecimal(4),
                    Stock = dr.GetInt32(5)
                };
                temporal.Add(p);
            }
            dr.Close(); cn.Close();
            return temporal;
        }
        public ActionResult UsuarioPorNombre(string nombre = "")
        {
            return View(UsuariosNombre(nombre));
        }
        public ActionResult ProductoPorCategoria(int idCodigo = 0)
        {
            ViewBag.categorias = new SelectList(Categorias(), "idCodigo", "Descripcion");
            return View(ProductoCategoria(idCodigo));
        }
        public ActionResult Index()
        {
            return View(Usuarios());
        }
        public ActionResult IndexProducto()
        {
            return View(Productos());
        }
        public ActionResult Indexcate()
        {
            return View(Categorias());
        }
    }
}