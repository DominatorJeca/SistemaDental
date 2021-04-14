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
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public string IdPacientes{get; set;}
        public string NombreDoctor { get; set; }
        public string ApellidoDoctor { get; set; }

        public string IdDoctor { get; set; }
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set;}

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
                    citas.Add(new ClaseCitas { IdDoctor = reader["id_empleado"].ToString(), NombreDoctor = reader["nombre"].ToString(), ApellidoDoctor = reader["apellido"].ToString() });
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
    }
}
