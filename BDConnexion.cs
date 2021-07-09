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
        private static string connectionString = @"server = (local)\SQLEXPRESS; Initial Catalog = clinicaDental; Integrated Security = True";
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
    }
}
