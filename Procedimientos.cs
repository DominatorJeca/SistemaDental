using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaDental
{
     class Procedimientos
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();

        SqlDataReader reader;
        public List<ClaseTratamiento> mostrarTratamientos()
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarTratamiento";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
                //ClaseTratamiento tratamiento = null;

                while (reader.Read())
                {
                    /*tratamiento = new ClaseTratamiento();
                    tratamiento.NombreTratamiento = reader["nombre"].ToString();
                    tratamiento.IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString());*/
                    tratamientos.Add(new ClaseTratamiento { NombreTratamiento = reader["nombre"].ToString(), IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString()) });
                }
                return tratamientos;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();
            }

        }
            #region usurio

            public Usuario BuscarUsuario(string id)
            {
                //objeto que contendrá los datos del usuario
                Usuario usuario = new Usuario();
                try
                {
                    command.Connection = con.Open();
                    //crear el comando SQL
                    command.CommandText = "VerificarExistenciaEmpleado";

                    command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@user", id);
                reader = command.ExecuteReader();

                using (reader)
                    {
                        while (reader.Read())
                        {
                            //Obtener valores del usuario
                            usuario.Id = Convert.ToString(reader["id_empleado"]);
                            usuario.Nombre = Convert.ToString(reader["nombre"]);
                            usuario.Contraseña = Convert.ToString(reader["contraseña"]);
                            usuario.Estado = Convert.ToBoolean(reader["estado"]);
                            usuario.Administrador = Convert.ToBoolean(reader["administrador"]);
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
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
                }

            }


            public void IngresarUsuario(Usuario usuario)
            {   //abrir la cadena de conexion
                command.Connection = con.Open();

                try
                {
                    //crear el comando SQL
                    command.CommandText = "IngresoEmpleados";
                    command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@id", usuario.Id);
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@correo", usuario.Correo);
                    command.Parameters.AddWithValue("@puesto", usuario.Puesto);
                    command.Parameters.AddWithValue("@genero", usuario.Genero);
                    command.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@estado", usuario.Estado);
                    command.Parameters.AddWithValue("@administrador", usuario.Administrador);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {    //Cerrar conexion 
                command.Parameters.Clear();
          
                command.Connection = con.Close();
                }
            }




            public void EditarUsuario(Usuario usuario)
            {
                command.Connection = con.Open();

                try
                {
                    //crear el comando SQL
                    command.CommandText = "EditarEmpleados"; 
                    command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@id", usuario.Id);
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@correo", usuario.Correo);
                    command.Parameters.AddWithValue("@puesto", usuario.Puesto);
                    command.Parameters.AddWithValue("@genero", usuario.Genero);
                    command.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@estado", usuario.Estado);
                    command.Parameters.AddWithValue("@administrador", usuario.Administrador);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {    //Cerrar conexion 
                command.Parameters.Clear();
   
                command.Connection = con.Close();
                }
            }


            public void EliminarUsuario(string idUsuario)
            {
                command.Connection = con.Open();

                try
                {
                    //crear el comando SQL
                    command.CommandText = "EliminarUsuario";
                   command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@id", idUsuario);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {    //Cerrar conexion 
              
                command.Parameters.Clear();
                command.Connection = con.Close();
                }

            }

            public void RestaurarUsuario(string idUsuario)
            {
                command.Connection = con.Open();

                try
                {
                    //crear el comando SQL
                    command.CommandText = "RestaurarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@id", idUsuario);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {    //Cerrar conexion 
             
                command.Parameters.Clear();
                command.Connection = con.Close();
                }

            }

            public void PrivilegioUsuario(string idUsuario)
            {
                command.Connection = con.Open();

                try
                {
                    //crear el comando SQL
                    command.CommandText = "PrivilegioAdministrador";
                    command.CommandType = CommandType.StoredProcedure;
                    //Establecer los valores de parametros
                    command.Parameters.AddWithValue("@id", idUsuario);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {    //Cerrar conexion
               
                command.Parameters.Clear();
                command.Connection = con.Close();
            
                }

            }


            public List<Usuario> MostrarUsuarios()
            {
                // Inicializar una lista vacía de usuarios
                List<Usuario> usuarios = new List<Usuario>();


                try
                {
                    command.Connection = con.Open();

                    //crear el comando SQL
                    command.CommandText = "MostrarEmpleadosActivos";

                    command.CommandType = CommandType.StoredProcedure;
                  reader = command.ExecuteReader();


                // Obtener los datos de los puestos
               
                    while (reader.Read())
                        usuarios.Add(new Usuario
                        {
                            Id = Convert.ToString(reader["id_empleado"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Correo = reader["correo"].ToString(),
                            Puesto = Convert.ToInt32(reader["idpuesto"]),
                            PuestoNombre = MostrarPuesto(Convert.ToInt32(reader["idpuesto"])),
                            Genero = reader["genero"].ToString(),
                            Contraseña = reader["contraseña"].ToString(),
                            Estado = Convert.ToBoolean(reader["estado"]),
                            Administrador = Convert.ToBoolean(reader["administrador"])
                        });
                

                return usuarios;
            }
            catch (Exception e)
                {
                   throw e;
                }
                finally
                {
                // Cerrar la conexión

                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
                

                }
            }


            public List<Usuario> MostrarUsuariosDesactivos()
            {
                // Inicializar una lista vacía de usuarios
                List<Usuario> usuarios = new List<Usuario>();


                try
                {
                    command.Connection = con.Open();

                    //crear el comando SQL    

                    command.CommandText = "MostrarEmpleadosDesactivos";

                    command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                // Obtener los datos de los puestos
                using (reader)
                    {
                        while (reader.Read())
                            usuarios.Add(new Usuario
                            {
                                Id = Convert.ToString(reader["id_empleado"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Puesto = Convert.ToInt32(reader["idpuesto"]),
                                PuestoNombre = MostrarPuesto(Convert.ToInt32(reader["idpuesto"])),
                                Genero = reader["genero"].ToString(),
                                Contraseña = reader["contraseña"].ToString(),
                                Estado = Convert.ToBoolean(reader["estado"]),
                                Administrador = Convert.ToBoolean(reader["administrador"])
                            });
                    }

                    return usuarios;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                // Cerrar la conexión
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();
                }
            }
        #endregion

        #region Puesto

        public List<Puesto> MostrarPuestos()
        {
            // Inicializar una lista vacía de puestos
            List<Puesto> puestos = new List<Puesto>();


            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "MostrarPuesto";

                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                // Obtener los datos de los puestos
                using (reader)
                {
                    while (reader.Read())
                        puestos.Add(new Puesto { Id = Convert.ToInt32(reader["id_puesto"]), NombrePuesto = reader["nombrePuesto"].ToString() });
                }

                return puestos;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                command.Parameters.Clear();
                reader.Close();
                command.Connection = con.Close();
            }

        }


        public string MostrarPuesto(int id )
        {
            // Inicializar una lista vacía de puestos
            string puesto;

            BDConnexion con1 = new BDConnexion();
            SqlCommand command1 = new SqlCommand();

       

            try
            {
                command1.Connection = con1.Open();
                //crear el comando SQL
                command1.CommandText = "MostrarUnPuesto";

                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@id", id);
  
                // Obtener los datos de los puestos
                using (SqlDataReader rdr = command1.ExecuteReader())
                {
                    rdr.Read();

                    puesto = rdr["nombrePuesto"].ToString();
                    
                }

                
                return puesto;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
              
                command1.Parameters.Clear();
                command1.Connection = con1.Close();
            }

        }



        #endregion
      

    }
}
