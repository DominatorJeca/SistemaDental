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
        Procedimientos proc = new Procedimientos();

        // Propiedades
        public int Id_transaccion { get; set; }

        public string Tipo_transacción { get; set; }

        public float Cantidad { get; set; }

        public DateTime Fecha  { get; set; }

        public float Dinero_disponible { get; set; }

        // Constructores

        public ClaseCaja() { }
        

        public ClaseCaja(int id_transaccion, string tipo_transaccion, float cantidad, DateTime fecha, float dinero_disponible)
        {
            Id_transaccion = id_transaccion;
            Tipo_transacción = tipo_transaccion;
            Cantidad = cantidad;
            Fecha = fecha;
            Dinero_disponible = dinero_disponible;

        }

        public List<ClaseCaja> MostrarCaja()
        {
            return proc.MostrarCaja();
        }


        public void IngresarCaja(ClaseCaja caja)
        {
           proc.IngresarCaja(caja);
        }

    }

}
    


