using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace SistemaDental
{
    public class Procedimientos
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
          List<ClaseTratamiento> mostrarTratamientos()
        {
            try
            {
                command.Connection = con.Open();
                command.CommandText = ("MostrarTratamiento");
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
                //ClaseTratamiento tratamiento = null;

                while (reader.Read())
                {
                    /*tratamiento = new ClaseTratamiento();
                    tratamiento.NombreTratamiento = reader["nombre"].ToString();
                    tratamiento.IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString());*/
                    tratamientos.Add(new ClaseTratamiento {NombreTratamiento=reader["nombre"].ToString(), IdTratamiento=Convert.ToInt32(reader["id_tratamiento"].ToString()) });
                }
                return tratamientos;
            }
            catch 
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
