using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDental
{
    public class ClaseInventario
    {
        private static string connectionString = @"server = .; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);



        //propiedades
        public int IdMaterial { get; set; }

        public string NombreMaterial { get; set; }

        public int Cantidad { get; set; }
        public DateTime fechaVenc { get; set; }
        public float precio { get; set; }

        public string NombreTratamiento { get; set; }

        public int CantidadUsada { get; set; }
        public ClaseInventario()
        {

        }






        public void actualizarCantidad(ClaseInventario inventario)
        {

            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("EditarInventario", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", inventario.NombreMaterial);
                command.Parameters.AddWithValue("@cantidad", inventario.Cantidad);
                command.Parameters.AddWithValue("@id", inventario.IdMaterial);
                command.ExecuteNonQuery();
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
