using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DSWI.Models
{
    public class UsuarioLista
    {
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public bool Administrador { get; set; }
    }
}