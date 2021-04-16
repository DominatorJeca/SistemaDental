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

        public string Fecha  { get; set; }

        public double Dinero_disponible { get; set; }

        // Constructores

        public ClaseCaja() { }
        

        public ClaseCaja(int id_transaccion, string tipo_transaccion, int cantidad, string fecha, double dinero_disponible)
        {
            Id_transaccion = id_transaccion;
            Tipo_transacción = tipo_transaccion;
            Cantidad = cantidad;
            Fecha = fecha;
            Dinero_disponible = dinero_disponible;

        }

       
        public List<ClaseCaja> MostrarCaja()
        {
            List<ClaseCaja> list = new List<ClaseCaja>();

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("MostrarTransaccion", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;


                // Obtener los datos de Caja
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new ClaseCaja { Id_transaccion = Convert.ToInt32(rdr["id_transaccion"]), Tipo_transacción = Convert.ToString(rdr["tipo_transacción"]), Cantidad = Convert.ToInt32(rdr["cantidad"]), Dinero_disponible = Convert.ToInt32(rdr["dinero_disponible"]), Fecha = System.DateTime.Now.ToShortDateString()});
                    }
                }

                return list;
                    
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


        public void IngresarCaja(ClaseCaja caja, string transaccion)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("IngresoTransaccion", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                // sqlCommand.Parameters.AddWithValue("@id_transaccion", caja.Id_transaccion);
                sqlCommand.Parameters.AddWithValue("@tipo_transacción", transaccion);
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
    


