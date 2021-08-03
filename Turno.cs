using System;

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
