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

        //Propiedad para extraer todos los ID almacenados en la BD
        public string id_paciente { get; set; }

        public DateTime Fecha { get; set; }
        public int IdHistorial { get; set; }
        public string IdPaciente { get; set; }
        public string IdTratamiento { get; set; }


        //Arreglo tipo string para almacenar los datos extraidos de la BD
        string[] datosPaciente = new string[10];


        public ClasePaciente() { }

        //propiedad con el parámetro id_paciente
        public ClasePaciente(string id)
        {
            id_paciente = id;
        }

        /// <summary>
        /// Metodo para mostrar los pacientes existentes
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

        /// <summary>
        /// Metodo para almacenar en un arreglo los datos del paciente seleccionado
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
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

        /// <summary>
        /// Metodo para actualizar los datos del paciente seleccionado
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
        public void ActualizarDatosPaciente(string id, string nombre, string apellido, string genero, string telefono, int edad)
        {

            try
            {//Abrir la conexion sql
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("EditarPacientes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", apellido);
                sqlCommand.Parameters.AddWithValue("@telefono", telefono);
                sqlCommand.Parameters.AddWithValue("@edad", edad);
                sqlCommand.Parameters.AddWithValue("@genero", genero);

                //Ejecucion del comando
                SqlDataReader reader = sqlCommand.ExecuteReader();
            }

            catch (Exception e) { throw e; }

            finally { sqlConnection.Close(); }

            
        }

        public List<ClasePaciente> MostrarHistorial(string id)
        {
            sqlConnection.Open();

            try
            {

                SqlCommand sqlCommand = new SqlCommand("MostrarHistorial", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idpaciente", id);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<ClasePaciente> TestList = new List<ClasePaciente>();
                ClasePaciente test = null;

                while (reader.Read())
                {
                    test = new ClasePaciente();
                    test.IdHistorial = int.Parse(reader["id_historial"].ToString());
                    test.IdPaciente = reader["id_paciente"].ToString();
                    test.Fecha = DateTime.Parse(reader["fecha"].ToString());
                    test.IdTratamiento = reader["id_tratamiento"].ToString();
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
                sqlConnection.Close();
            }
        }

    }
}