using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    public class Usuario
    {
        //variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono{ get; set; }

        public string Correo{get; set;}

        public int Puesto { get; set; }

        public string Genero{ get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }

        public bool Administrador{ get; set; }


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
        public Usuario BuscarUsuario(string id)
        {
            //objeto que contendrá los datos del usuario
            Usuario usuario = new Usuario();
            try
            {
                sqlConnection.Open();
                //crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("VerificarExistenciaEmpleado", sqlConnection);
                
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                sqlCommand.Parameters.AddWithValue("@user", id);

                using (SqlDataReader LeeUsuario = sqlCommand.ExecuteReader())
                {
                    while (LeeUsuario.Read())
                    {
                        //Obtener valores del usuario
                        usuario.Id = Convert.ToString(LeeUsuario["id_empleado"]);
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

        /// <summary>
        /// Ingresar usuario a la tabla empleados
        /// </summary>
        /// <param name="usuario">Datos del usuario</param>

        public void IngresarUsuario(Usuario usuario)
        {
            sqlConnection.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("IngresoEmpleados", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                sqlCommand.Parameters.AddWithValue("@id",usuario.Id);
                sqlCommand.Parameters.AddWithValue("@nombre", usuario.Nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", usuario.Apellido);
                sqlCommand.Parameters.AddWithValue("@telefono", usuario.Telefono);
                sqlCommand.Parameters.AddWithValue("@correo", usuario.Correo);
                sqlCommand.Parameters.AddWithValue("@puesto", usuario.Puesto);
                sqlCommand.Parameters.AddWithValue("@genero", usuario.Genero);
                sqlCommand.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                sqlCommand.Parameters.AddWithValue("@estado", usuario.Estado);
                sqlCommand.Parameters.AddWithValue("@administrador", usuario.Administrador);
                sqlCommand.ExecuteNonQuery();

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
