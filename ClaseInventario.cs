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
    class ClaseInventario
    {
        private static string connectionString = @"server = (local); Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        

        //propiedades
        public int IdMaterial { get; set; }

        public string NombreMaterial { get; set; }

        public int Cantidad { get; set; }
        public DateTime fechaVenc { get; set; }
        public float precio { get; set; }
        public ClaseInventario()
        {

        }

        public List<ClaseInventario> MostrarInventario()
        {
            sqlConnection.Open();

            try
            {

                SqlCommand command = new SqlCommand("MostrarInventario", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseInventario> TestList = new List<ClaseInventario>();
                ClaseInventario test = null;

                while (reader.Read())
                {
                    test = new ClaseInventario();
                    test.IdMaterial = int.Parse(reader["id_material"].ToString());
                    test.NombreMaterial = reader["nombre"].ToString();
                    test.Cantidad = int.Parse(reader["cantidad"].ToString());
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

        
        public DataTable mostrarUsoTratamiento(int idmaterial)
        {
            sqlConnection.Open();
            try
            {
                DataTable tabla = new DataTable();
                SqlCommand command = new SqlCommand("MostrarUso",sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idmaterial);
                tabla.Load(command.ExecuteReader());
                return tabla;
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

        public void actualizarCantidad(ClaseInventario inventario)
        {
           
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("EditarInventario", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", inventario.NombreMaterial);
                command.Parameters.AddWithValue("@cantidad", inventario.Cantidad);
                command.Parameters.AddWithValue("@id",inventario.IdMaterial);
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
