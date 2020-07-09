using AppPrueba.Models;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPrueba.Controllers
{
    public class CiudadController : Controller
    {
        public List<Ciudad> getAllCiudades()
        {
            // Realizar la conexion a la base de datos
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_get_all_ciudades";
            OracleParameter par1 = new OracleParameter();
            par1.OracleDbType = OracleDbType.RefCursor;
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);
            cmd.ExecuteNonQuery();
            OracleRefCursor cursor = (OracleRefCursor)par1.Value;
            OracleDataReader dr = cursor.GetDataReader();
   
            List<Ciudad> listCiudades = new List<Ciudad>();
            while (dr.Read())
            {
                Ciudad ciudad = new Ciudad
                {
                    id_ciudad = Convert.ToInt32(dr["id_ciudad"]),
                    nombre = dr["nombre"].ToString(),
                    ubicacion_geografica = dr["ubicacion_geografica"].ToString()
                };
                listCiudades.Add(ciudad);
            }

            cone.Close();
            par1.Dispose();
            cmd.Dispose();
            cone.Dispose();

            return listCiudades;
        }
        // GET: Ciudad
   
        public ActionResult Index()
        {
            CiudadController cm = new CiudadController(); 
            List<Ciudad> list = cm.getAllCiudades();
            return View(list);
        }

        // this part is for insert 

        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Ciudad ciudad)
        {
            // Realizar la conexion a la base de dato
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_insert_ciudad";

            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = ciudad.nombre;

            cmd.Parameters.Add("pi_ubicacion_geografica", OracleDbType.Varchar2).Value = ciudad.ubicacion_geografica;

            cmd.ExecuteNonQuery();
            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            CiudadController cont = new CiudadController();

            string men = cont.returnMssg(ciudad);

            ViewBag.mensaje = men;
            return View();
        }
        // GET: Ciudad/Edit/5
        public ActionResult Update(int id)
        {
            CiudadController cont = new CiudadController();
            return View(cont.getAllCiudades().Find(model => model.id_ciudad == id));
        }

        [HttpPost]
        public ActionResult Update(int id, Ciudad ciudad)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_update_ciudad";

            cmd.Parameters.Add("pi_id_ciudad", id);

            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = ciudad.nombre;

            cmd.Parameters.Add("pi_ubicacion_geografica", OracleDbType.Varchar2).Value = ciudad.ubicacion_geografica;
            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return RedirectToAction("Index");


        }
      

        // GET: Ciudad/Delete/5
        public void DeleteData(int id)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = " wilson1.pgk_municipalidad_empleados.sp_delete_ciudad";
            cmd.Parameters.Add("id_ciudad", id);

            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();
        }

        public ActionResult Delete(int id)
        {
            CiudadController cont = new CiudadController();
            return View(cont.getAllCiudades().Find(model => model.id_ciudad == id));

        }
        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult CheckDelete(int id)
        {

            CiudadController cont = new CiudadController();
            cont.DeleteData(id);
            return RedirectToAction("Index");
   
        }

        public string returnMssg(Ciudad ciudad)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = " wilson1.pgk_municipalidad_empleados.sp_mensaje";
            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = ciudad.nombre;

            cmd.Parameters.Add("po_mensaje", OracleDbType.Varchar2,500);
            cmd.Parameters["po_mensaje"].Direction = ParameterDirection.Output;
           
            System.Diagnostics.Debug.WriteLine(mensaje);

            System.Diagnostics.Debug.WriteLine("Hola");
            Console.WriteLine(mensaje);
            cmd.ExecuteNonQuery();
            
            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return mensaje;
        }
    }
}
