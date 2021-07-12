using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Configuration;
using System.Windows;

namespace SistemaDental
{
     class Procedimientos
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();

        SqlDataReader reader;

        #region Citas - Procedimientos
        public List<ClaseCitas> CitaMostrarTratamiento()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarTratamiento";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();
                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString()), NombreTratamiento = reader["nombre"].ToString() });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitamostrarIdPacientes()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarPacientes";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> pacientes = new List<ClaseCitas>();


                while (reader.Read())
                {

                    pacientes.Add(new ClaseCitas { IdPacientes = reader["id_paciente"].ToString() });
                }
                return pacientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitaMostrarEmpleado()
        {
            command.Connection = con.Open();

            try
            {
                command.CommandText = "MostrarDoctores";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdDoctor = reader["id_empleado"].ToString(), NombreDoctor = reader["nombre"].ToString() });
                }
                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void CitaAgendarCita(ClaseCitas cita)
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "IngresoCitas";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idempleado", cita.IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", cita.IdPacientes);
                command.Parameters.AddWithValue("@fecha", cita.fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", cita.IdTratamiento);
                reader = command.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void CitaEditarCita(ClaseCitas cita)
        {
            command.Connection = con.Open();

            try
            {
                command.CommandText = "EditarCitas";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idcita", cita.IdCita);
                command.Parameters.AddWithValue("@idempleado", cita.IdDoctor);
                command.Parameters.AddWithValue("@idpaciente", cita.IdPacientes);
                command.Parameters.AddWithValue("@fecha", cita.fechaCita);
                command.Parameters.AddWithValue("@idtratamiento", cita.IdTratamiento);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseCitas> CitaMostrarCitas()
        {
            command.Connection = con.Open();
            try
            {
                command.CommandText = "MostrarCitas";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                List<ClaseCitas> citas = new List<ClaseCitas>();

                while (reader.Read())
                {
                    citas.Add(new ClaseCitas { IdCita = Convert.ToInt32(reader["idcita"].ToString()), IdPacientes = reader["idpaciente"].ToString(), NombrePaciente = reader["paciente"].ToString(), ApellidoPaciente = reader["apepac"].ToString(), NombreDoctor = reader["nombredoc"].ToString(), NombreTratamiento = reader["tratamiento"].ToString(), fechaCita = Convert.ToDateTime(reader["fecha"].ToString()) });
                }

                return citas;
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        #endregion
        #region Inventario - Procedimientos

        public List<ClaseInventario> InventarioMostrarInventario()
        {
            command.Connection = con.Open();
            try
            {

                command.CommandText = "MostrarInventario";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

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
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public DataTable InventarioMostrarUsoTratamiento(int idmaterial)
        {
            command.Connection = con.Open();
            try
            {
                DataTable tabla = new DataTable();
                command.CommandText =  "MostrarUso";
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
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public void InventarioActualizarCantidad(ClaseInventario inventario)
        {
            command.Connection = con.Open();
            try
            {
                con.Open();
                command.CommandText = "EditarInventario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", inventario.NombreMaterial);
                command.Parameters.AddWithValue("@cantidad", inventario.Cantidad);
                command.Parameters.AddWithValue("@id", inventario.IdMaterial);
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex);
                throw;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
        #endregion
        #region tratamiento
        public List<ClaseTratamiento> mostrarTratamientos()
{
            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarTratamiento";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
                while (reader.Read())
                {
                    tratamientos.Add(new ClaseTratamiento { NombreTratamiento = reader["nombre"].ToString(), IdTratamiento = Convert.ToInt32(reader["id_tratamiento"].ToString()) });
                }
                return tratamientos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex);
                throw;
            }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

        public List<ClaseTratamiento> mostrarIdPacientes()
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarPacientes";
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseTratamiento> pacientes = new List<ClaseTratamiento>();
                ClaseTratamiento paciente = null;

                while (reader.Read())
                {
                    paciente = new ClaseTratamiento();
                    paciente.IdPaciente = reader["id_paciente"].ToString();
                    pacientes.Add(paciente);
                }
                return pacientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();

                command.Parameters.Clear();
                command.Connection=con.Close();
            }
        }
        public List<ClaseTratamiento> mostrarMateriales(int idtratamiento)
        {

            try
            {

                command.Connection = con.Open();
                command.CommandText = "materialnecesario";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idtratamiento);
                SqlDataReader reader = command.ExecuteReader();

                List<ClaseTratamiento> materiales = new List<ClaseTratamiento>();
                ClaseTratamiento material = null;

                while (reader.Read())
                {
                    material = new ClaseTratamiento();
                    material.NombreMaterial = reader["nombre"].ToString();
                    material.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());
                    materiales.Add(material);
                }
                return materiales;
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Close();

                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }
         public void IngresarAlHistorial(ClaseTratamiento tratamiento)
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "IngresoHistorial";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpaciente", tratamiento.IdPaciente);
                command.Parameters.AddWithValue("@fecha", Convert.ToDateTime(tratamiento.fecha));
                command.Parameters.AddWithValue("@idtratamiento", tratamiento.IdTratamiento);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }

         public void ActualizarMaterialDisponible(ClaseTratamiento tratamiento)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "actualizarCantidad";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cantidad", tratamiento.Cantidad);
                command.Parameters.AddWithValue("@nombre", tratamiento.NombreMaterial);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Parameters.Clear();
                command.Connection= con.Close();
            }
        }
        #endregion
        #region Pacientes
        public List<ClasePaciente> MostrarPacientes()
        {

            List<ClasePaciente> paciente = new List<ClasePaciente>();


            try
            {
                command.Connection = con.Open();

                //crear el comando SQL
                command.CommandText = "Mostrarpacientes";

                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();
                // Obtener los datos de los puestos
                using (reader)
                {
                    while (reader.Read())

                        paciente.Add(new ClasePaciente { Id_paciente = reader["id_paciente"].ToString(), NombrePaciente = reader["nombre"].ToString(), ApellidoPaciente = reader["apellido"].ToString(), Fecha = ((DateTime)reader["Fechanac"]), Telefono = reader["telefono"].ToString(), Genero = reader["genero"].ToString() });
                }

                return paciente;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                reader.Close();
                command.Connection=con.Close();
                command.Parameters.Clear();
            }
        }
        public void ActualizarDatosPaciente(ClasePaciente paciente)
        {

            try
            {//Abrir la conexion sql
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "EditarPacientes";
                command.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                command.Parameters.AddWithValue("@id", paciente.Id_paciente);
                command.Parameters.AddWithValue("@nombre", paciente.NombrePaciente);
                command.Parameters.AddWithValue("@apellido", paciente.ApellidoPaciente);
                command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                command.Parameters.AddWithValue("@fechanac", paciente.FechaNac);
                command.Parameters.AddWithValue("@genero", paciente.Genero);
                command.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                throw e;
            }

            finally
            {

                command.Connection = con.Close();
                command.Parameters.Clear();
            }


        }

        public List<ClasePaciente> MostrarHistorial(ClasePaciente paciente)
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarHistorial";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpaciente", paciente.Id_paciente);
                reader = command.ExecuteReader();

                List<ClasePaciente> pacientes = new List<ClasePaciente>();

                while (reader.Read())
                {
                    pacientes.Add(new ClasePaciente { Fecha = Convert.ToDateTime(reader["fecha"].ToString()), NombreTratamiento = reader["tratamiento"].ToString() });
                }

                return pacientes;
            }
            catch (Exception e)

            {
                throw e;
            }
            finally
            {
                reader.Close();
                command.Connection = con.Close();
                command.Parameters.Clear();
            }
        }

        public void AgregarPaciente(ClasePaciente paciente)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "IngresoPacientes";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", paciente.Id_paciente);
                command.Parameters.AddWithValue("@nombre", paciente.NombrePaciente);
                command.Parameters.AddWithValue("@apellido", paciente.ApellidoPaciente);
                command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                command.Parameters.AddWithValue("@FechaNac", paciente.FechaNac);
                command.Parameters.AddWithValue("@genero", paciente.Genero);
                command.Parameters.AddWithValue("@estado", 1);
                command.ExecuteNonQuery();
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
          #endregion
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
            try
            {
                // Inicializar una lista vacía de puestos
                List<Puesto> puestos = new List<Puesto>();
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
        #region caja

public List<ClaseCaja> MostrarCaja()
{
    List<ClaseCaja> list = new List<ClaseCaja>();
            try
            {
                command.Connection = con.Open();
                command.CommandText = "MostrarTransaccion";
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader();

                // Obtener los datos de Caja
                using (reader)
                {
                    while (reader.Read())
                    {
                        list.Add(new ClaseCaja { Id_transaccion = Convert.ToInt32(reader["id_transaccion"]), Tipo_transacción = Convert.ToString(reader["tipo_transacción"]), Cantidad = (float)Convert.ToDecimal(reader["cantidad"]), Dinero_disponible = (float)Convert.ToDecimal(reader["dinero_disponible"]), Fecha = Convert.ToDateTime(reader["fecha"].ToString()) });
                    }
                }

                return list;

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                reader.Close();

                command.Parameters.Clear();
                command.Connection = con.Close();
            }
        }


        public void IngresarCaja(ClaseCaja caja)
        {


            try
            {
                command.Connection = con.Open();
                command.CommandText = "IngresoTransaccion";
                command.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                command.Parameters.AddWithValue("@tipo", caja.Tipo_transacción);
                command.Parameters.AddWithValue("@cantidad", caja.Cantidad);
                command.Parameters.AddWithValue("@fecha", caja.Fecha);
                command.Parameters.AddWithValue("@dinerodisponible", caja.Dinero_disponible);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {    //Cerrar conexion

                command.Connection= con.Close();
                command.Parameters.Clear();
            }
        }

        #endregion


    }
}
