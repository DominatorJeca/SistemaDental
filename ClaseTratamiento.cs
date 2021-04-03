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
    class ClaseTratamiento
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public int IdPaciente { get; set; }
        public int IdTratamiento { get; set; }
        public int IdMaterial { get; set; }
        public string NombreTratamiento { get; set; }
        public string NombreMaterial { get; set; }
        public int Cantidad { get; set; }
       
        public ClaseTratamiento() { }

        public List<ClaseTratamiento> mostrarTratamientos()
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("MostrarTratamiento", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
                ClaseTratamiento tratamiento = null;

                while (reader.Read())
                {
                    tratamiento = new ClaseTratamiento();
                    tratamiento.NombreTratamiento = reader["nombre"].ToString();
                    tratamiento.IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString());
                    tratamientos.Add(tratamiento);
                }
                return tratamientos;
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
