﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar Namespaces Necesarios para Conexion SQL
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaDental
{
    class ClasePaciente
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public ClasePaciente()
        { }

        public int Id { get; set; }

        public string NombrePaciente { get; set; }


        public ClasePaciente(int id, string nombrePaciente) {
            Id = id;
            NombrePaciente = nombrePaciente;
        }

        public List<ClasePaciente> MostrarPacientes()
        {
            //Inicializar una lista vacia de pacientes
            List<ClasePaciente> paciente = new List<ClasePaciente>();

            try
            {
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mostrarpacientes", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                //Obtener los datos de los pacientes
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while ((rdr.Read()))
                        paciente.Add(new ClasePaciente(Id = Convert.ToInt32(rdr["id_paciente"]), NombrePaciente = rdr["nombre"].ToString()));

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
                sqlConnection.Close();
            }

        }


    }
}
