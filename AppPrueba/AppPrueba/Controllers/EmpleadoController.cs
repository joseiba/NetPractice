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
    public class EmpleadoController : Controller
    {
        public List<Empleado> getAllEmpleados()
        {
            // Realizar la conexion a la base de datos
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_get_all_empleados";
            OracleParameter par1 = new OracleParameter();
            par1.OracleDbType = OracleDbType.RefCursor;
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);
            cmd.ExecuteNonQuery();
            OracleRefCursor cursor = (OracleRefCursor)par1.Value;
            OracleDataReader dr = cursor.GetDataReader();

            List<Empleado> listEmpleado = new List<Empleado>();
            while (dr.Read())
            {
                Empleado emp = new Empleado
                {
                    cedula = Convert.ToInt32(dr["cedula"]),
                    nombre = Convert.ToString(dr["nombre"]),
                    apellido = Convert.ToString(dr["apellido"]),
                    telefono = Convert.ToString(dr["telefono"]),
                    direccion = Convert.ToString(dr["direccion"]),
                    salario = Convert.ToInt32(dr["salario"]),
                    id_cargo = Convert.ToInt32(dr["id_cargo"]),
                    id_municipalidad = Convert.ToInt32(dr["id_municipalidad"]),
                };
                listEmpleado.Add(emp);
            }
            cone.Close();
            par1.Dispose();
            cmd.Dispose();
            cone.Dispose();

            return listEmpleado;
        }
        // GET: Ciudad

        public ActionResult Index()
        {
            EmpleadoController cont = new EmpleadoController();
            List<Empleado> list = cont.getAllEmpleados();
            return View(list);
        }

        // this part is for insert 

        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Empleado emp)
        {
            // Realizar la conexion a la base de dato
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_insert_empleados";

            cmd.Parameters.Add("pi_cedula", OracleDbType.Int32).Value = emp.cedula;
            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = emp.nombre;
            cmd.Parameters.Add("pi_apellido", OracleDbType.Varchar2).Value = emp.apellido;
            cmd.Parameters.Add("pi_telefono", OracleDbType.Varchar2).Value = emp.telefono;
            cmd.Parameters.Add("pi_direccion", OracleDbType.Varchar2).Value = emp.direccion;
            cmd.Parameters.Add("pi_cargo", OracleDbType.Int32).Value = emp.id_cargo;
            cmd.Parameters.Add("pi_municipalidad", OracleDbType.Int32).Value = emp.id_municipalidad;

            cmd.ExecuteNonQuery();
            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return RedirectToAction("Index");
        }
        // GET: Ciudad/Edit/5
        public ActionResult Update(int cedula)
        {
            EmpleadoController cont = new EmpleadoController();
            return View(cont.getAllEmpleados().Find(model => model.cedula == cedula));
        }

        [HttpPost]
        public ActionResult Update(int cedula, Empleado emp)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wilson1.pgk_municipalidad_empleados.sp_update_empleados";

            cmd.Parameters.Add("pi_cedula", cedula);
            cmd.Parameters.Add("pi_nombre", OracleDbType.Varchar2).Value = emp.nombre;
            cmd.Parameters.Add("pi_apellido", OracleDbType.Varchar2).Value = emp.apellido;
            cmd.Parameters.Add("pi_telefono", OracleDbType.Varchar2).Value = emp.telefono;
            cmd.Parameters.Add("pi_direccion", OracleDbType.Varchar2).Value = emp.direccion;
            cmd.Parameters.Add("pi_cargo", OracleDbType.Int32).Value = emp.id_cargo;
            cmd.Parameters.Add("pi_municipalidad", OracleDbType.Int32).Value = emp.id_municipalidad;

            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();

            return RedirectToAction("Index");


        }


        // GET: Ciudad/Delete/5
        public void DeleteData(int cedula)
        {
            Conexion.Conexion extD11 = new Conexion.Conexion();
            OracleConnection cone = extD11.getConexion();
            cone.Open(); // Abre la conexion a la base de datos 
            OracleCommand cmd = cone.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = " wilson1.pgk_municipalidad_empleados.sp_delete_empleados";
            cmd.Parameters.Add("pi_cedula", cedula);

            cmd.ExecuteNonQuery();

            cone.Close();
            cmd.Dispose();
            cone.Dispose();
        }

        public ActionResult Delete(int cedula)
        {
            EmpleadoController cont = new EmpleadoController();
            return View(cont.getAllEmpleados().Find(model => model.cedula == cedula));

        }
        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult CheckDelete(int cedula)
        {

            EmpleadoController cont = new EmpleadoController();
            cont.DeleteData(cedula);
            return RedirectToAction("Index");

        }
    }
}