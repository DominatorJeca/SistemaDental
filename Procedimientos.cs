using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SistemaDental
{
     class Procedimientos
    {
        BDConnexion con = new BDConnexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
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
                    tratamientos.Add(new ClaseTratamiento {NombreTratamiento=reader["nombre"].ToString(), IdTratamiento=Convert.ToInt32(reader["id_tratamiento"].ToString()) });
                }
                return tratamientos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error"+ex);
                throw;
           }
            finally
            {
                reader.Close();
                command.Parameters.Clear();
                command.Connection=con.Close();
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
                command.Connection = con.Close();
                command.Parameters.Clear();
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
