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
    public class Puesto
    {
        Procedimientos proce = new Procedimientos();
        //variable miembro


        //Propiedades
        public int Id { get; set; }

        public string NombrePuesto { get; set; }

        public Puesto() { }

        public Puesto(int id,string nombrePuesto) {
            Id = id;
            NombrePuesto = nombrePuesto;
        }

        /// <summary>
        /// Mostrar los puestos
        /// </summary>
        /// <returns>Lista de todos los datos de los puestos</returns>
     
    }
}
