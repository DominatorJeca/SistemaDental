using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SistemaDental
{
    class ClaseProcedimiento
    {
        public void ActualizarTurno(ClaseTurno turno)
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
    }
}
