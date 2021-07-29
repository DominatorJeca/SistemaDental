using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace SistemaDental
{
    public class BDConnexion
    {
        private static string connectionString = @"Server=tcp:sistemassps.database.windows.net,1433;Database=SistemaDental;User ID=SistemasSps;Password=grupito7@;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";
        protected SqlConnection sqlConnection = new SqlConnection(connectionString);
        public void CheckConnection()
        {
            try
            {
                sqlConnection.Open();
                MessageBox.Show("Estas connectado a " + sqlConnection.Database.ToString() + " exitosamente!!");
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection Open()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }

        public SqlConnection Close()
        {

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            return sqlConnection;
        }
    }
}

