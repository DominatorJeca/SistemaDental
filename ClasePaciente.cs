﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    public class ClasePaciente
    {

        //variable miembro
        BDConnexion con = new BDConnexion();
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        //private static string connectionString = @"server = .; Initial Catalog = clinicaDental; Integrated Security = True; MultipleActiveResultSets=true";
        //private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedad para extraer todos los ID almacenados en la BD
        public string Id_paciente { get; set; }

        public string NombrePaciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNac { get; set; }
        public int Edad { get; set; }

        public string Genero { get; set; }

        public string Identidad { get; set; }

       
        public int IdHistorial { get; set; }

       
        public string Correo { get; set; }
        public string Estado { get; set; }
        public string Paciente { get; set; }
        public string NombreTratamiento { get; set; }
        public string Doctor { get; set; }
        public DateTime FechaCita { get; set; }



        //Arreglo tipo string para almacenar los datos extraidos de la BD


        public ClasePaciente() { }

        //propiedad con el parámetro id_paciente


        /// <summary>
        /// Metodo para mostrar los pacientes existentes
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>



        /// <summary>
        /// Metodo para actualizar los datos del paciente seleccionado
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>

        public void AgregarPaciente(ClasePaciente paciente)
        {
            procedimiento.AgregarPaciente(paciente);
        }

        public List<ClasePaciente> MostrarPacientes()
        {
            return procedimiento.MostrarPacientes();
        }

        public List<ClasePaciente> MostrarHistorial(ClasePaciente paciente)
        {
            return procedimiento.MostrarHistorial(paciente);
        }

        public void ActualizarDatosPaciente(ClasePaciente paciente)
        {
            procedimiento.ActualizarDatosPaciente(paciente);
        }

        }
}