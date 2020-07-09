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
    public class MunicipalidadController : Controller
    {
        public List<Municipalidad> getAllMunicipalidades()
        {
            // Realizar la conexion a la base de datos
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_get_all_municipalidades";
            OracleParameter par1 = new OracleParameter();
            par1.OracleDbType = OracleDbType.RefCursor;
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);
            cmd.ExecuteNonQuery();
            OracleRefCursor cursor = (OracleRefCursor)par1.Value;
            OracleDataReader dr = cursor.GetDataReader();

            List<Municipalidad> listMunicipalidades = new List<Municipalidad>();
            while (dr.Read())
            {
                Municipalidad muni = new Municipalidad
                {
                    id_municipalidad = Convert.ToInt32(dr["id_municipalidad"]),
                    nombre_municipalidad = Convert.ToString(dr["nombre_municipalidad"]),
                    direccion = Convert.ToString(dr["direccion"]),
                    telefono = Convert.ToString(dr["telefono"]),
                    correo_electronico = Convert.ToString(dr["correo_electronico"]),
                    id_ciudad = Convert.ToInt32(dr["id_ciudad"])
                };
                listMunicipalidades.Add(muni);
            }
            cone.Close();
            par1.Dispose();
            cmd.Dispose();
            cone.Dispose();

            return listMunicipalidades;
        }
        // GET: Ciudad

        public ActionResult Index()
        {
            MunicipalidadController cont = new MunicipalidadController();
            List<Municipalidad> list = cont.getAllMunicipalidades();
            return View(list);
        }

        // this part is for insert 

        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Municipalidad muni)
        {
            // Realizar la conexion a la base de dato
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_insert_municipalidades";

            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = muni.nombre_municipalidad;
            cmd.Parameters.Add("pi_direccion", OracleDbType.Varchar2).Value = muni.direccion;
            cmd.Parameters.Add("pi_telefono", OracleDbType.Varchar2).Value = muni.telefono;
            cmd.Parameters.Add("pi_correo_electronico", OracleDbType.Varchar2).Value = muni.correo_electronico;
            cmd.Parameters.Add("pi_id_ciudad", OracleDbType.Varchar2).Value = muni.id_ciudad;


            cmd.ExecuteNonQuery();
            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return RedirectToAction("Index");
        }
        // GET: Ciudad/Edit/5
        public ActionResult Update(int id)
        {
            MunicipalidadController cont = new MunicipalidadController();
            return View(cont.getAllMunicipalidades().Find(model => model.id_municipalidad == id));
        }

        [HttpPost]
        public ActionResult Update(int id, Municipalidad muni)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_update_municipalidad";

            cmd.Parameters.Add("pi_id_municipalidad", id);
            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = muni.nombre_municipalidad;
            cmd.Parameters.Add("pi_direccion", OracleDbType.Varchar2).Value = muni.direccion;
            cmd.Parameters.Add("pi_telefono", OracleDbType.Varchar2).Value = muni.telefono;
            cmd.Parameters.Add("pi_correo_electronico", OracleDbType.Varchar2).Value = muni.correo_electronico;
            cmd.Parameters.Add("pi_id_ciudad", OracleDbType.Varchar2).Value = muni.id_ciudad;

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
            cmd.CommandText = " wilson1.pgk_municipalidad_empleados.sp_delete_municipalidad";
            cmd.Parameters.Add("pi_id_municipalidad", id);

            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();
        }

        public ActionResult Delete(int id)
        {
            MunicipalidadController cont = new MunicipalidadController();
            return View(cont.getAllMunicipalidades().Find(model => model.id_municipalidad == id));

        }
        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult CheckDelete(int id)
        {

            MunicipalidadController cont = new MunicipalidadController();
            cont.DeleteData(id);
            return RedirectToAction("Index");

        }
    }
}