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

        public string NombrePaciente { get; set; }

        public ClasePaciente() { }

        public ClasePaciente(string id, string nombrePaciente)
        {
            id_paciente = id;
            NombrePaciente = nombrePaciente;
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
                        paciente.Add(new ClasePaciente { id_paciente = Convert.ToString(rdr["id_paciente"]), NombrePaciente = rdr["nombre"].ToString() });
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
    }
}