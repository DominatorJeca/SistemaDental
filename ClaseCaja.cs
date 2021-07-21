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
        private static string connectionString = @"server = DESKTOP-8MP5H5G; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true"; 
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int Id_transaccion { get; set; }

        public string Tipo_transacción { get; set; }

        public float Cantidad { get; set; }

        public DateTime Fecha  { get; set; }

        public float Dinero_disponible { get; set; }

        // Constructores

        public ClaseCaja() { }
        

        public ClaseCaja(int id_transaccion, string tipo_transaccion, float cantidad, DateTime fecha, float dinero_disponible)
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
                        list.Add(new ClaseCaja { Id_transaccion = Convert.ToInt32(rdr["id_transaccion"]), Tipo_transacción = Convert.ToString(rdr["tipo_transacción"]), Cantidad = (float)Convert.ToDecimal(rdr["cantidad"]), Dinero_disponible = (float)Convert.ToDecimal(rdr["dinero_disponible"]), Fecha = Convert.ToDateTime(rdr["fecha"].ToString())});
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


        public void IngresarCaja(ClaseCaja caja)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("IngresoTransaccion", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                sqlCommand.Parameters.AddWithValue("@tipo",caja.Tipo_transacción);
                sqlCommand.Parameters.AddWithValue("@cantidad", caja.Cantidad);
                sqlCommand.Parameters.AddWithValue("@fecha", caja.Fecha);
                sqlCommand.Parameters.AddWithValue("@dinerodisponible", caja.Dinero_disponible);
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
    


