using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaDental
{
    public class ClaseCitas
    {

        public ClaseProcedimiento proc = new ClaseProcedimiento();

        public int IdCita { get; set; }

        public string IdPacientes { get; set; }
        public int detalleCita { get; set; }
    
        public string IdEmpleado { get; set; }
        public string NombreDoctor { get; set; }

        public string Preciototal { get; set; }
        public string trtamientoprecio { get; set; }
        public string nombreTramientoindividual { get; set; }
        public string Nombre_Id_paciente { get; set; }
       
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set; }
        
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string Observaciones { get; set; }
        public DateTime fechaCita { get; set; }


        public void InsertarDetalleCita(int cita, int IdTratamiento, float trtamientoprecio)
        {
            proc.InsertarDetalleCita(IdTratamiento, trtamientoprecio, cita);
        }



            public List<ClaseCitas> mostrarIdPacientes()
        {
            return proc.mostrarIdPacientes();
        }

        public List<ClaseCitas> MostrarEmpleado()
        {
            return proc.MostrarEmpleado();
        }

        public List<ClaseCitas> MostrarTratamiento()
        {
            return proc.MostrarTratamiento();
        }

        public void AgendarCita(ClaseCitas cita)
        {
            proc.AgendarCita(cita);
        }

        public void EditarCita(ClaseCitas cita)
        {
            proc.EditarCita(cita);
        }

        public List<ClaseCitas> MostrarCitas()
        {
           return proc.MostrarCitas();
        }
        public List<ClaseCitas> Mostrartratmientos(int idcita)
        {
            return proc.Mostrartratmientos(idcita);
        }


        public void EliminarCita(int cita)
        {
            proc.EliminarCita(cita);
        }

        public List<ClaseCitas> mostrarPacientesconcitas()
        {

            return proc.mostrarPacientesconcitas();


        }

        
             public List<ClaseCitas> mostrarPacientesxcitas(int citas)
        {

            return proc.mostrarPacientesxcitas(citas);


        }

        public List<ClaseCitas> mostrarPacientes()
        {

             return proc.mostrarPacientes();

        }
         public void mostraridtrtamientos(ClaseCitas cita,int idtratamiento)
        {
            proc.mostraridtrtamientos(cita,idtratamiento);

        }

        public void eliminardetallecita (int idcita)
        {

            proc.eliminardetallecita(idcita);
        }
    }
}
