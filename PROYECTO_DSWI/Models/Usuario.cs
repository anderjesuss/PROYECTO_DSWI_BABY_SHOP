using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROYECTO_DSWI.Models
{
    public class Usuario
    {
        public int idUsuario {  get; set; }
        public string NomUsuario { get; set; }
        public string ApeUsuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool EsAdministrador { get; set; }
        public string ConfirmarClave { get; set; }
    }
}