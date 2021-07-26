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
    public class Puesto
    {

        //variable miembro
        private static string connectionString = @"server =DESKTOP-E7NG9FA\SQLEXPRESS; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int Id { get; set; }

        public string NombrePuesto { get; set; }

        public Puesto() { }

        public Puesto(int id,string nombrePuesto) {
            Id = id;
            NombrePuesto = nombrePuesto;
        }

        /// <summary>
        /// Mostrar los puestos
        /// </summary>
        /// <returns>Lista de todos los datos de los puestos</returns>
        public List<Puesto> MostrarPuestos()
        {
            // Inicializar una lista vacía de puestos
            List<Puesto> puestos = new List<Puesto>();
           

            try
            {
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarPuesto", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Obtener los datos de los puestos
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        puestos.Add(new Puesto { Id = Convert.ToInt32(rdr["id_puesto"]), NombrePuesto = rdr["nombrePuesto"].ToString()});
                }

                return puestos;
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

        public string MostrarPuesto(int id)
        {
            // Inicializar una lista vacía de puestos
            string puesto;


            try
            {
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarUnPuesto", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);

                // Obtener los datos de los puestos
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    rdr.Read();
                      
                    puesto = rdr["nombrePuesto"].ToString();
                }

                return puesto;
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
