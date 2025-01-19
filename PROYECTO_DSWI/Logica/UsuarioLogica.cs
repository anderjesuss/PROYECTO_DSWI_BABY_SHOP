using PROYECTO_DSWI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace PROYECTO_DSWI.Logica
{
    public class UsuarioLogica
    {
        //Conexion
        string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        //
        private static UsuarioLogica _instancia = null;

        public UsuarioLogica()
        {

        }

        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new UsuarioLogica();
                }

                return _instancia;
            }
        }

        public Usuario Obtener(string correo, string clave)
        {
            Usuario objeto = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_validarUsuario", cn);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Usuario()
                            {
                                idUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                                NomUsuario = dr["Nombres"].ToString(),
                                ApeUsuario = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                EsAdministrador = Convert.ToBoolean(dr["EsAdministrador"].ToString())
                            };

                        }
                    }

                }
                catch (Exception ex)
                {
                    objeto = null;
                }
            }
            return objeto;
        }
        public int Registrar(Usuario oUsuario)
        {
            int respuesta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_registrarUsuarios", cn);
                    cmd.Parameters.AddWithValue("nombres", oUsuario.NomUsuario);
                    cmd.Parameters.AddWithValue("apellidos", oUsuario.ApeUsuario);
                    cmd.Parameters.AddWithValue("correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("clave", oUsuario.Clave);
                    cmd.Parameters.AddWithValue("esadmin", oUsuario.EsAdministrador);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }
    }
}