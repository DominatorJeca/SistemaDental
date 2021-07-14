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
    public class ClasePaciente
    {

        //variable miembro
        Procedimientos proc = new Procedimientos();

        //Propiedad para extraer todos los ID almacenados en la BD
        public string Id_paciente { get; set; }

        public string NombrePaciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNac { get; set; }
        public int Edad { get; set; }

        public string Genero { get; set; }


        public DateTime Fecha { get; set; }
        public int IdHistorial { get; set; }

        public string NombreTratamiento { get; set; }


        //Arreglo tipo string para almacenar los datos extraidos de la BD
        

        public ClasePaciente() { }

        //propiedad con el parámetro id_paciente
        

        /// <summary>
        /// Metodo para mostrar los pacientes existentes
        /// </summary>
        /// <returns>Lista de todos los datos de los pacientes</returns>
      

    }
}