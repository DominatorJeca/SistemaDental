﻿using System;
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
    class ClaseInventario
    {
        Procedimientos proc = new Procedimientos();
        

        //propiedades
        public int IdMaterial { get; set; }

        public string NombreMaterial { get; set; }

        public int Cantidad { get; set; }

        public ClaseInventario()
        {

        }

   
    }
}
