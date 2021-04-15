using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Agregar los namespaces necesarios para conectarse a SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    class ClaseCaja
    {

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public int id_transaccion { get; set; }

        public string tipo_transacción { get; set; }

        public int cantidad { get; set; }

        public int fecha { get; set; }

        public double dinero_disponible { get; set; }


        public ClaseCaja()
        {

        }

        public List<ClaseCaja> MostrarCaja()
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("MostrarTransaccion", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseCaja> TestList = new List<ClaseCaja>();
                ClaseCaja test = null;

                while (reader.Read())
                {
                    test = new ClaseCaja();
                    test.id_transaccion = int.Parse(reader["id_transaccion"].ToString());
                    test.tipo_transacción = reader["MostrarTransaccion"].ToString();
                    test.cantidad = int.Parse(reader["cantidad"].ToString());
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

        public List<ClaseCaja> IngresarDatos()
        {
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("IngresoTransaccion", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseCaja> TestList = new List<ClaseCaja>();
                ClaseCaja test = null;

                while (reader.Read())
                {
                    test = new ClaseCaja();
                    test.id_transaccion = int.Parse(reader["id_transaccion"].ToString());
                    test.tipo_transacción = reader["MostrarTransaccion"].ToString();
                    test.cantidad = int.Parse(reader["cantidad"].ToString());
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
    


