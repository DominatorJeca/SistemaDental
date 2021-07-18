using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace SistemaDental
{
 
    class ClaseProcedimiento : BDConnexion
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        public bool Modificaciones(string Cadena)
        {
            SqlCommand cmd = new SqlCommand(Cadena, sqlConnection);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();

            sqlConnection.Dispose();
            cmd.Dispose();
            return true;
        }

        #region Compras

        public void EditarCompra(int CompraID, int InventarioID, int EmpleadoID, DateTime FechaCompra, DateTime FechaVenci, double precio, int cantidad)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "EditarCompra";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompraID", CompraID);
                command.Parameters.AddWithValue("@InventarioID", InventarioID);
                command.Parameters.AddWithValue("@EmpleadoID", EmpleadoID);
                command.Parameters.AddWithValue("@FechaCompra", FechaCompra);
                command.Parameters.AddWithValue("@FechaVencimiento", FechaVenci);
                command.Parameters.AddWithValue("@PrecioCompra", precio);
                command.Parameters.AddWithValue("@cantidad", cantidad);


            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }


        }

        public void InsertarCompra(int InventarioID, int EmpleadoID, DateTime FechaCompra, DateTime FechaVenci, double precio, int cantidad)
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = "IngresarCompra";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@InventarioID", InventarioID);
                command.Parameters.AddWithValue("@EmpleadoID", EmpleadoID);
                command.Parameters.AddWithValue("@FechaCompra", FechaCompra);
                command.Parameters.AddWithValue("@FechaVencimiento", FechaVenci);
                command.Parameters.AddWithValue("@PrecioCompra", precio);
                command.Parameters.AddWithValue("@cantidad", cantidad);


            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
        #endregion

    }
}
