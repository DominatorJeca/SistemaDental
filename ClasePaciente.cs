using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    public class ClasePaciente
    {

        //variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public string id_paciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public string TelefonoPaciente { get; set; }

        public string EdadPaciente { get; set; }

        public string GeneroPaciente { get; set; }

        string[] datosPaciente = new string[10];


        public ClasePaciente() { }

        public ClasePaciente(string id)
        {
            id_paciente = id;

        }

        /// <summary>
        /// Mostrar los pacientes
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
        public List<ClasePaciente> MostrarPacientes()
        {

            List<ClasePaciente> paciente = new List<ClasePaciente>();


            try
            {
                sqlConnection.Open();

                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mostrarpacientes", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Obtener los datos de los puestos
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        paciente.Add(new ClasePaciente { id_paciente = Convert.ToString(rdr["id_paciente"])});
                }

                return paciente;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        //public List<ClasePaciente> LlenarDatosPaciente(string idPaciente)
        public string[] LlenarDatosPaciente(string idPaciente)
        {

            try {

                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarPacienteEspecifico", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", idPaciente);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    datosPaciente[0] = reader["id_paciente"].ToString();
                    datosPaciente[1] = reader["nombre"].ToString();
                    datosPaciente[2] = (reader["apellido"].ToString());
                    datosPaciente[3] = (reader["genero"].ToString());
                    datosPaciente[4] = (reader["telefono"].ToString());
                    datosPaciente[5] = (reader["edad"].ToString());

                }

                return (datosPaciente);


            }
            catch (Exception e) { throw e; }
            finally { sqlConnection.Close(); }


        }




    }
}