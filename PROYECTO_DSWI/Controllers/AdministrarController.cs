using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Ocsp;
using PROYECTO_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PROYECTO_DSWI.Controllers
{
    public class AdministrarController : Controller
    {
        // GET: Administrar
        string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        IEnumerable<CATEGORIA> Categorias()
        {
            List<CATEGORIA> temporal = new List<CATEGORIA>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_lista_categoria", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CATEGORIA c = new CATEGORIA()
                {
                    IdCategoria = dr.GetInt32(0),
                    Descripcion = dr.GetString(1)
                };
                temporal.Add(c);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<PRODUCTO> Productos()
        {
            List<PRODUCTO> temporal = new List<PRODUCTO>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_lista_productos", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PRODUCTO p = new PRODUCTO()
                {
                    IdProducto = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    IdMarca = dr.GetInt32(2),
                    IdCategoria = dr.GetInt32(3),
                    Precio = dr.GetDecimal(4),
                    Stock = dr.GetInt32(5),
                };
                temporal.Add(p);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<MARCA> Marcas()
        {
            List<MARCA> temporal = new List<MARCA>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_lista_marcas", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MARCA m = new MARCA()
                {
                    IdMarca = dr.GetInt32(0),
                    Descripcion = dr.GetString(1),
                };
                temporal.Add(m);
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        string AgregarCategoria(CATEGORIA cate)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_inserta_categoria @descripcion", cn);
                cmd.Parameters.AddWithValue("@descripcion", cate.Descripcion);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado la Categoría {cate.Descripcion}";
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string ActualizarCategoria(CATEGORIA cate)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_actualizar_categoria @codCate, @descripcion", cn);
                cmd.Parameters.AddWithValue("@codCate", cate.IdCategoria);
                cmd.Parameters.AddWithValue("@descripcion", cate.Descripcion);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado la Categoría de código {cate.IdCategoria}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string EliminarCategoria(int id)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_elimina_categoria @codigo", cn);
                cmd.Parameters.AddWithValue("@codigo", id);
                cmd.ExecuteNonQuery();

                mensaje = $"Se ha eliminado la Categoría de código {id}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string AgregarMarca(MARCA mar)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_inserta_marca @descripcion", cn);
                cmd.Parameters.AddWithValue("@descripcion", mar.Descripcion);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado la Marca {mar.Descripcion}";
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string ActualizarMarca(MARCA marca)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_actualizar_marca @codmarca, @descripcion", cn);
                cmd.Parameters.AddWithValue("@codmarca", marca.IdMarca);
                cmd.Parameters.AddWithValue("@descripcion", marca.Descripcion);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado la Marca de código {marca.IdMarca}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string EliminarMarca(int id)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_eliminar_marca @codigo", cn);
                cmd.Parameters.AddWithValue("@codigo", id);
                cmd.ExecuteNonQuery();

                mensaje = $"Se ha eliminado la Marca de código {id}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string AgregarProducto(PRODUCTO prod)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_inserta_producto @nombre, @marca, @categoria, @precio, @stock, @imagen", cn);
                cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@marca", prod.MARCA?.IdMarca);
                cmd.Parameters.AddWithValue("@categoria", prod.CATEGORIA?.IdCategoria);
                cmd.Parameters.AddWithValue("@precio", prod.Precio);
                cmd.Parameters.AddWithValue("@stock", prod.Stock);
                cmd.Parameters.AddWithValue("@imagen", prod.RutaImagen);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado el Producto {prod.Nombre}";
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string ActualizarProducto(PRODUCTO producto)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_actualizar_producto @codproducto, @nombre, @codmarca, @codcate, @precio, @stock", cn);
                cmd.Parameters.AddWithValue("@codproducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@codmarca", producto.MARCA?.IdMarca);
                cmd.Parameters.AddWithValue("@codcate", producto.CATEGORIA?.IdCategoria);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado el Producto de código {producto.IdProducto}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        string EliminarProducto(int id)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_eliminar_producto @codproducto", cn);
                cmd.Parameters.AddWithValue("@codproducto", id);
                cmd.ExecuteNonQuery();

                mensaje = $"Se ha eliminado el Producto de código {id}";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.Close(); }
            return mensaje;
        }
        CATEGORIA buscar(int codigo)
        {
            return Categorias().FirstOrDefault(s => s.IdCategoria == codigo);
        }
        MARCA buscarMarca(int codigo)
        {
            return Marcas().FirstOrDefault(s => s.IdMarca == codigo);
        }
        PRODUCTO buscarProducto(int codigo)
        {
            return Productos().FirstOrDefault(s => s.IdProducto == codigo);
        }
        public ActionResult Index()
        {
            return View(Categorias());
        }
        public ActionResult IndexMarca()
        {
            return View(Marcas());
        }
        public ActionResult IndexProducto()
        {
            return View(Productos());
        }
        [HttpPost] public ActionResult CreateCategoria(CATEGORIA cate)
        {
            ViewBag.mensaje = AgregarCategoria(cate);
            return View(cate);
        }
        [HttpPost] public ActionResult EditCategoria(CATEGORIA cate)
        {
            ViewBag.mensaje = ActualizarCategoria(cate);
            return View(cate);
        }
        [HttpPost] public ActionResult CreateMarca(MARCA mar)
        {
            ViewBag.mensaje = AgregarMarca(mar);
            return View(mar);
        }
        [HttpPost] public ActionResult EditMarca(MARCA marca)
        {
            ViewBag.mensaje = ActualizarMarca(marca);
            return View(marca);
        }
        [HttpPost] public ActionResult CreateProducto(PRODUCTO prod)
        {
            ViewBag.mensaje = AgregarProducto(prod);
            ViewBag.marcas = new SelectList(Marcas(), "IdMarca", "Descripcion", prod.IdMarca);
            ViewBag.categorias = new SelectList(Categorias(), "IdCategoria", "Descripcion", prod.IdCategoria);
            return View(prod);
        }
        [HttpPost] public ActionResult EditProducto(PRODUCTO producto)
        {
            ViewBag.mensaje = ActualizarProducto(producto);
            return View(producto);
        }
        public ActionResult CreateCategoria()
        {
            return View(new CATEGORIA());
        }
        public ActionResult CreateMarca()
        {
            return View(new MARCA());
        }
        public ActionResult CreateProducto()
        {
            ViewBag.marcas = new SelectList(Marcas(), "IdMarca", "Descripcion");
            ViewBag.categorias = new SelectList(Categorias(), "IdCategoria", "Descripcion");
            return View(new PRODUCTO());
        }
        public ActionResult EditCategoria(int codigo)
        {
            CATEGORIA cate = buscar(codigo);
            return View(cate);
        }
        public ActionResult EditMarca(int codigo)
        {
            MARCA marca = buscarMarca(codigo);
            return View(marca);
        }
        public ActionResult EditProducto(int codigo)
        {
            PRODUCTO producto = buscarProducto(codigo);
            return View(producto);
        }
        public ActionResult DeleteCategoria(int id)
        {
            ViewBag.mensaje = EliminarCategoria(id);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMarca(int id)
        {
            ViewBag.mensaje = EliminarMarca(id);
            return RedirectToAction("IndexMarca");
        }
        public ActionResult DeleteProducto(int id)
        {
            ViewBag.mensaje = EliminarProducto(id);
            return RedirectToAction("IndexProducto");
        }
    }
}