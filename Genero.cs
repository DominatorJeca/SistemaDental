using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDental
{
   public  class Genero
    {
        public int Id { get; set; }

        public string NombreGenero { get; set; }

        public Genero() { }

        public Genero(int id, string nombreGenero)
        {
            Id = id;
            NombreGenero= nombreGenero;
        }


    }
}
