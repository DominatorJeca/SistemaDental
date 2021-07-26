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
        private static string connectionString = @"server = .; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedad para extraer todos los ID almacenados en la BD
        public int Id_paciente { get; set; }
        public string correo { get; set; }
        public string identificacionPaciente { get; set; }
        public string NombrePaciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNac { get; set; }
        public int Edad { get; set; }

        public string Genero { get; set; }


        public DateTime Fecha { get; set; }
        public int IdHistorial { get; set; }

        public string NombreTratamiento { get; set; }


        //Arreglo tipo string para almacenar los datos extraidos de la BD


        public ClasePaciente() { }

        //propiedad con el parámetro id_paciente


        /// <summary>
        /// Metodo para mostrar los pacientes existentes
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
     /*   public List<ClasePaciente> MostrarPacientes()
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

                        paciente.Add(new ClasePaciente { Id_paciente = rdr["id_paciente"].ToString(), NombrePaciente=rdr["nombre"].ToString(), ApellidoPaciente=rdr["apellido"].ToString(), Fecha=((DateTime)rdr["Fechanac"]), Telefono=rdr["telefono"].ToString(), Genero=rdr["genero"].ToString()});
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
        /// Metodo para actualizar los datos del paciente seleccionado
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
        public void ActualizarDatosPaciente(ClasePaciente paciente)
        {

            try
            {//Abrir la conexion sql
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("EditarPacientes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                sqlCommand.Parameters.AddWithValue("@id", paciente.Id_paciente);
                sqlCommand.Parameters.AddWithValue("@nombre", paciente.NombrePaciente);
                sqlCommand.Parameters.AddWithValue("@apellido", paciente.ApellidoPaciente);
                sqlCommand.Parameters.AddWithValue("@telefono",paciente.Telefono);
                sqlCommand.Parameters.AddWithValue("@fechanac", paciente.FechaNac);
                sqlCommand.Parameters.AddWithValue("@genero", paciente.Genero);
                sqlCommand.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                sqlConnection.Close();
            }


        }

        public List<ClasePaciente> MostrarHistorial(ClasePaciente paciente)
        {
            sqlConnection.Open();

            try
            {

                SqlCommand sqlCommand = new SqlCommand("MostrarHistorial", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idpaciente", paciente.Id_paciente);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                List<ClasePaciente> pacientes = new List<ClasePaciente>();

                while (reader.Read())
                {
                    pacientes.Add(new ClasePaciente { Fecha = Convert.ToDateTime(reader["fecha"].ToString()), NombreTratamiento = reader["tratamiento"].ToString() });
                }

                return pacientes;
            }
            catch (Exception e)

            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void AgregarPaciente( ClasePaciente paciente)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("IngresoPacientes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", paciente.Id_paciente);
                sqlCommand.Parameters.AddWithValue("@nombre", paciente.NombrePaciente);
                sqlCommand.Parameters.AddWithValue("@apellido", paciente.ApellidoPaciente);
                sqlCommand.Parameters.AddWithValue("@telefono", paciente.Telefono);
                sqlCommand.Parameters.AddWithValue("@FechaNac", paciente.FechaNac);
                sqlCommand.Parameters.AddWithValue("@genero", paciente.Genero);
                sqlCommand.Parameters.AddWithValue("@estado", 1);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }*/

    }
}
