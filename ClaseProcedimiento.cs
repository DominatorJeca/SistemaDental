using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SistemaDental
{
    class ClaseProcedimiento
    {
        BDConexion con = new BDConexion();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        #region Turno - Procedimientos

        public void ActualizarTurno(Turno turno)
        {

            try
            {//Abrir la conexion sql
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Turno_Actualizar";
                command.CommandType = CommandType.StoredProcedure;

                //Definir las variables del procedimiento mediante los parametros obtenidos
                command.Parameters.AddWithValue("@AgendaID", turno.AgendaID);
                command.Parameters.AddWithValue("@UsuarioID", turno.UsuarioID);
                command.Parameters.AddWithValue("@ComienzoTurno", turno.ComienzoTurno);
                command.Parameters.AddWithValue("@FinalTurno", turno.FinalTurno);

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

        public void AgregarTurno(Turno turno)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Turno_Insertar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsuarioID", turno.UsuarioID);
                command.Parameters.AddWithValue("@ComienzoTruno", turno.ComienzoTurno);
                command.Parameters.AddWithValue("@FinalTurno", turno.FinalTurno);

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

        public void EliminarTurno(int AgendaID)
        {

            try
            {
                command.Connection = con.Open();
                command.CommandText = "sp_Turno_Eliminar";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AgendaID", AgendaID);


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
        #endregion

        #region Usuario-Procedimientos

        public Usuario BuscarUsuario(string user, string contra)
        {
            //objeto que contendrá los datos del usuario
            Usuario usuario = new Usuario();
            try
            {
                command.Connection = con.Open();
                //crear el comando SQL
                command.CommandText = "sp_Usuario_VerificarLogin";
                
                command.CommandType = CommandType.StoredProcedure;
                //Establecer los valores de parametros
                command.Parameters.AddWithValue("@nombreUsuario", user);
                command.Parameters.AddWithValue("@contrasena", contra);
                reader = command.ExecuteReader();

                
                using (reader)
                {
                    while (reader.Read())
                    {
                        //Obtener valores del usuario

                        usuario.Nombre = Convert.ToString(reader["Nombre"]);
                        usuario.Apellido = Convert.ToString(reader["Apellido"]);
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
            #endregion


        }

    }
}
