using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDental
{
    public class Turno
    {
        public int AgendaID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime ComienzoTurno { get; set; }
        public DateTime FinalTurno { get; set; }
        
        public Turno() { }
    }
}
