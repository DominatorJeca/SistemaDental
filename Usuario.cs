using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//nameSpace de conexion con SQL Server
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    public class Usuario
    {
        //variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Setting.SistemaDentalConnectionString"].ConnectionString;
        private SqlConnection sqlConnection= new SqlConnection(connectionString);
    }
}
