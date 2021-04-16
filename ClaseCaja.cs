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
    public class ClaseCaja
    {

        //Variable Miembro
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int Id_transaccion { get; set; }

        public string Tipo_transacción { get; set; }

        public int Cantidad { get; set; }

        public int Fecha { get; set; }

        public double Dinero_disponible { get; set; }

        // Constructores

        public ClaseCaja() { }
        

        public ClaseCaja(int id_transaccion, string tipo_transaccion, int cantidad, int fecha, double dinero_disponible)
        {
            Id_transaccion = id_transaccion;
            Tipo_transacción = tipo_transaccion;
            Cantidad = cantidad;
            Fecha = fecha;
            Dinero_disponible = dinero_disponible;

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


        public void IngresarCaja(ClaseCaja caja)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("IngresoTransaccion", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                sqlCommand.Parameters.AddWithValue("@id_transaccion", caja.Id_transaccion);
                sqlCommand.Parameters.AddWithValue("@tipo_transacción", caja.Tipo_transacción);
                sqlCommand.Parameters.AddWithValue("@cantidad", caja.Cantidad);
                sqlCommand.Parameters.AddWithValue("@fecha", caja.Fecha);
                sqlCommand.Parameters.AddWithValue("@dinero_disponible", caja.Dinero_disponible);
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {    //Cerrar conexion 
                sqlConnection.Close();
            }
        }

    }

}
    


