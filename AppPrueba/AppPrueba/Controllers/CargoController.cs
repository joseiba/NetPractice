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
    public class CargoController : Controller
    {
        public List<Cargo> getAllCargos()
        {
            // Realizar la conexion a la base de datos
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_get_all_cargos";
            OracleParameter par1 = new OracleParameter();
            par1.OracleDbType = OracleDbType.RefCursor;
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);
            cmd.ExecuteNonQuery();
            OracleRefCursor cursor = (OracleRefCursor)par1.Value;
            OracleDataReader dr = cursor.GetDataReader();

            List<Cargo> listCargo = new List<Cargo>();
            while (dr.Read())
            {
                Cargo car = new Cargo
                {
                    id_cargo = Convert.ToInt32(dr["id_cargo"]),
                    nombre_cargo = Convert.ToString(dr["nombre_cargo"]),
                    descripcion = Convert.ToString(dr["descripcion"])
                };
                listCargo.Add(car);
            }
            cone.Close();
            par1.Dispose();
            cmd.Dispose();
            cone.Dispose();

            return listCargo;
        }
        // GET: Ciudad

        public ActionResult Index()
        {
            CargoController cont = new CargoController();
            List<Cargo> list = cont.getAllCargos();
            return View(list);
        }

        // this part is for insert 

        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Cargo cargo)
        {
            // Realizar la conexion a la base de dato
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_insert_cargo";

            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = cargo.nombre_cargo;
            cmd.Parameters.Add("pi_descripciom", OracleDbType.Varchar2).Value = cargo.descripcion;

            cmd.ExecuteNonQuery();
            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return RedirectToAction("Index");
        }
        // GET: Ciudad/Edit/5
        public ActionResult Update(int id)
        {
            CargoController cont = new CargoController();
            return View(cont.getAllCargos().Find(model => model.id_cargo == id));
        }

        [HttpPost]
        public ActionResult Update(int id, Cargo cargo)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_update_cargo";

            cmd.Parameters.Add("pi_id_cargo", id);
            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = cargo.nombre_cargo;
            cmd.Parameters.Add("pi_descripciom", OracleDbType.Varchar2).Value = cargo.descripcion;

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
            cmd.CommandText = " wilson1.pgk_municipalidad_empleados.sp_delete_cargo";
            cmd.Parameters.Add("pi_id_cargo", id);

            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();
        }

        public ActionResult Delete(int id)
        {
            CargoController cont = new CargoController();
            return View(cont.getAllCargos().Find(model => model.id_cargo == id)); 

        }
        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult CheckDelete(int id)
        {

            CargoController cont = new CargoController();
            cont.DeleteData(id);
            return RedirectToAction("Index");

        }
    }
}