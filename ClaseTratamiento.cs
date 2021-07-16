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
    class ClaseTratamiento
    {
       // private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        //private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public string IdPaciente { get; set; }

        public string NombrePaciente { get; set; }
        public int IdTratamiento { get; set; }
        public int IdMaterial { get; set; }
        public string NombreTratamiento { get; set; }
        public string NombreMaterial { get; set; }
        public int Cantidad { get; set; }
        public string fecha { get; set; }
       
        public ClaseTratamiento() { }

    }
}
