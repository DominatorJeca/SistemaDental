using System;

// Agregar los namespaces necesarios para conectarse a SQL

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
        public string tratamientos { get; set; }
        public ClasePaciente paciente { get; set; }
        public Usuario Usuario { get; set; }


        // Constructores

        public ClaseCaja()
        {
        }


    }

}
