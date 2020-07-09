using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppPrueba.Conexion
{
    public class Conexion
    {
        private OracleConnection cone { get; set; }

        public OracleConnection getConexion()
        {
            if (cone == null)
            {
                string conexion = ConfigurationManager.AppSettings["connectionString"].ToString();
                cone = new OracleConnection(conexion);
            }
            return cone;
        }
    }
}