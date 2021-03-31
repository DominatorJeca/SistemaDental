using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//nameSpace de conexion con SQL Server
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace SistemaDental
{
    public class Usuario
    {
        //variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Setting.SistemaDentalConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Contraseña { get; set; }

        public bool Estado { get; set; }





        //Contructores

        public Usuario() { }

        public Usuario(string id, string nombre, string contraseña, bool estado)
        {
            Id = id;
            Nombre = nombre;
            Contraseña = contraseña;
            Estado = estado;
        }

        //Metodos
        /// <summary>
        /// Buscar si el ID pertenece a un usuario existente
        /// </summary>
        /// <param name="id">Identificacion de usuario</param>
        /// <returns>Datos del usuario</returns>
        public Usuario  BuscarUsuario(string id)
        {
            //objeto que contendrá los datos del usuario
            Usuario usuario = new Usuario();
            try
            {
               
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("VerificarExistenciaEmpleado",sqlConnection);
                //sqlCommand.CommandText = "VerificarExistenciaEmpleado";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader LeeUsuario= sqlCommand.ExecuteReader())
                {
                    while (LeeUsuario.Read())
                    { 
                        //Obtener valores del usuario
                        usuario.Id = Convert.ToString(LeeUsuario["idempleado"]);
                        usuario.Nombre = Convert.ToString(LeeUsuario["nombre"]);
                        usuario.Contraseña = Convert.ToString(LeeUsuario["contraseña"]);
                        usuario.Estado = Convert.ToBoolean(LeeUsuario["estado"]);
                    }
                }

                return usuario;
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
