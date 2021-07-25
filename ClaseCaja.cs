using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Agregar los namespaces necesarios para conectarse a SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDental
{
    public class ClaseCaja
    {

        //Variable Miembro
     

        // Propiedades
        public int Id_transaccion { get; set; }
        public float Cantidad { get; set; }
        public float Cobrado { get; set; }
        public float Abonado { get; set; }
        public DateTime FechaTrans { get; set; }
        public string UltimoAbono { get; set; }
        public float Dinero_disponible { get; set; }

        public ClaseCitas cita { get; set; }
        public string  tratamientos { get; set; }
        public ClasePaciente paciente { get; set; }
        public Usuario Usuario { get; set; }


        // Constructores

        public ClaseCaja()
        {
        }
        

    }

}
    


