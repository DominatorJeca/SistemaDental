using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaDental
{
    class ClaseCitas
    {
        private static string connectionString = @"server =  DESKTOP-8MP5H5G; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public int IdCita { get; set; }

        public string IdPacientes{get; set;}
        public string NombreDoctor { get; set; }

        public string IdDoctor { get; set; }
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set;}

        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }

        public DateTime fechaCita { get; set; }
        public List<ClaseCitas> mostrarIdPacientes()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("MostrarPacientes", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

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
                sqlConnection.Close();
            }

        }

        public List<ClaseCitas> MostrarEmpleado()
        {
            sqlConnection.Open();

            try{
                SqlCommand command = new SqlCommand("MostrarDoctores", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

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
                sqlConnection.Close();
            }
        }

        public List<ClaseCitas> MostrarTratamiento()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("MostrarTratamiento", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
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
                sqlConnection.Close();
            }
        }

        public void AgendarCita(ClaseCitas cita)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("IngresoCitas", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idempleado", IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", IdPacientes);
                command.Parameters.AddWithValue("@fecha", fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", IdTratamiento);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void EditarCita(ClaseCitas cita)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("EditarCitas", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", IdCita);
                command.Parameters.AddWithValue("@idempleado", IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", IdPacientes);
                command.Parameters.AddWithValue("@fecha", fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", IdTratamiento);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<ClaseCitas> MostrarCitas()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("MostrarCitas", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas {IdCita=Convert.ToInt32(reader["idcita"].ToString()), IdPacientes = reader["idpaciente"].ToString(), NombrePaciente=reader["paciente"].ToString(),ApellidoPaciente=reader["apepac"].ToString(),NombreDoctor=reader["nombredoc"].ToString(),NombreTratamiento = reader["tratamiento"].ToString(), fechaCita=Convert.ToDateTime(reader["fecha"].ToString()) });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void EliminarCita()
        {
            sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("EliminarCitas", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", IdCita);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
