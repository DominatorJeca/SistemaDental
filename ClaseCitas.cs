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
    class ClaseCitas
    {
        Procedimientos proc = new Procedimientos();

        public int IdCita { get; set; }

        public string IdPacientes{get; set;}
        public string NombreDoctor { get; set; }

        public string IdDoctor { get; set; }
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set;}

        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }

        public DateTime fechaCita { get; set; }

     
    }
}
