using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PROYECTO_DSWI.Models;
using System.Configuration;

namespace PROYECTO_DSWI.Datos
{
    public class Reportes
    {
        string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public List<ReporteVenta> ReporteVentas()
        {
            List<ReporteVenta> objLista = new List<ReporteVenta>();

            //Data Source=;Initial Catalog= ; User ID= ; Password=
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                string query = "usp_Reporte_Ventas";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteVenta()
                        {
                            mes = dr["mes"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }
            return objLista;
        }

        public List<ReporteProducto> ReporteProductos()
        {
            List<ReporteProducto> lista = new List<ReporteProducto>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            SqlCommand cmd = new SqlCommand("exec usp_Reporte_Producto", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ReporteProducto rp = new ReporteProducto()
                {
                    producto = dr.GetString(0),
                    cantidad = dr.GetInt32(1)
                };
                lista.Add(rp);
            }
            dr.Close();
            cn.Close();

            return lista;
        }
    }
}