using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Configuration;
using System.Windows;

namespace SistemaDental
{
    class Procedimientos
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        #region Citas - Procedimientos
        public List<ClaseCitas> CitaMostrarTratamiento()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarTratamiento";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString()), NombreTratamiento = reader["nombre"].ToString() });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitamostrarIdPacientes()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarPacientes";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> pacientes = new List<ClaseCitas>();


                while (reader.Read())
                {

                    pacientes.Add(new ClaseCitas { IdPacientes = reader["id_paciente"].ToString() });
                }
                return pacientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitaMostrarEmpleado()
        {
            command.Connection = con.Open();

            try
            {
                command.CommandText = "MostrarDoctores";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdDoctor = reader["id_empleado"].ToString(), NombreDoctor = reader["nombre"].ToString() });
                }
                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void CitaAgendarCita(ClaseCitas cita)
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "IngresoCitas";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idempleado", cita.IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", cita.IdPacientes);
                command.Parameters.AddWithValue("@fecha", cita.fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", cita.IdTratamiento);
                reader = command.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void CitaEditarCita(ClaseCitas cita)
        {
            command.Connection = con.Open();

            try
            {
                command.CommandText = "EditarCitas";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", cita.IdCita);
                command.Parameters.AddWithValue("@idempleado", cita.IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", cita.IdPacientes);
                command.Parameters.AddWithValue("@fecha", cita.fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", cita.IdTratamiento);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitaMostrarCitas()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarCitas";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdCita = Convert.ToInt32(reader["idcita"].ToString()), IdPacientes = reader["idpaciente"].ToString(), NombrePaciente = reader["paciente"].ToString(), ApellidoPaciente = reader["apepac"].ToString(), NombreDoctor = reader["nombredoc"].ToString(), NombreTratamiento = reader["tratamiento"].ToString(), fechaCita = Convert.ToDateTime(reader["fecha"].ToString()) });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        #endregion

        #region Inventario - Procedimientos

        public List<ClaseInventario> InventarioMostrarInventario()
        {
            command.Connection = con.Open();
            try
            {

                command.CommandText = "MostrarInventario";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseInventario> TestList = new List<ClaseInventario>();
                ClaseInventario test = null;

                while (reader.Read())
                {
                    test = new ClaseInventario();
                    test.IdMaterial = int.Parse(reader["id_material"].ToString());
                    test.NombreMaterial = reader["nombre"].ToString();
                    test.Cantidad = int.Parse(reader["cantidad"].ToString());
                    TestList.Add(test);
                }

                return TestList;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable InventarioMostrarUsoTratamiento(int idmaterial)
        {
            command.Connection = con.Open();
            try
            {
                DataTable tabla = new DataTable();
                command.CommandText =  "MostrarUso";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idmaterial);
                tabla.Load(command.ExecuteReader());
                return tabla;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void InventarioActualizarCantidad(ClaseInventario inventario)
        {
            command.Connection = con.Open();
            try
            {
                con.Open();
                command.CommandText = "EditarInventario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", inventario.NombreMaterial);
                command.Parameters.AddWithValue("@cantidad", inventario.Cantidad);
                command.Parameters.AddWithValue("@id", inventario.IdMaterial);
                reader = command.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
        #endregion
    }
}
