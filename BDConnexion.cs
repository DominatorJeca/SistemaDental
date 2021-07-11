using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SistemaDental
{
    class BDConnexion
    {
        private static string connectionString = @"server = (local); Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
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
