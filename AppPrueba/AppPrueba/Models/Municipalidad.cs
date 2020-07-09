using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPrueba.Models
{
    public class Municipalidad
    {
        public int id_municipalidad { get; set; }
        public string nombre_municipalidad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo_electronico { get; set; }
        public int id_ciudad { get; set; }
    }
}