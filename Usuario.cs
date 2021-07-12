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
    public class Usuario
    {
        //variable miembro


        Procedimientos proc = new Procedimientos();
        public Puesto puesto = new Puesto();
        //cadena de conexion
        

        //Propiedades
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono{ get; set; }

        public string Correo{get; set;}

        public int Puesto { get; set; }

        public string PuestoNombre { get; set; }

        public string Genero{ get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }

        public bool Administrador{ get; set; }


        //Contructores

        public Usuario() { }

        public Usuario(string id, string nombre, string apellido, string telefono, string correo,int puesto,string genero, string contraseña, bool estado, bool administrador)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Correo = correo;
            Puesto = puesto;
            Genero = genero;
            Contraseña = contraseña;
            Estado = estado;
            Administrador = administrador;
        }

        //Metodos
        /// <summary>
        /// Buscar si el ID pertenece a un usuario existente
        /// </summary>
        /// <param name="id">Identificacion de usuario</param>
        /// <returns>Datos del usuario</returns>
        public Usuario BuscarUsuario(string id)
        {
            return proc.BuscarUsuario(id);
        }
        public void IngresarUsuario(Usuario usuario)
        {
            proc.IngresarUsuario(usuario);
        }

        public void EditarUsuario(Usuario usuario)
        {
             proc.EditarUsuario(usuario);
        }
   
        public void EliminarUsuario(string idUsuario)
        {
             proc.EliminarUsuario(idUsuario);

        }

        public void RestaurarUsuario(string idUsuario)
        {
             proc.RestaurarUsuario(idUsuario);
        }
       
        public void PrivilegioUsuario(string idUsuario)
        {
             proc.PrivilegioUsuario(idUsuario);

        }

        public List<Usuario> MostrarUsuarios()
        {
            return proc.MostrarUsuarios();
        }


        public List<Usuario> MostrarUsuariosDesactivos()
        {
            return proc.MostrarUsuariosDesactivos();
        }
    }
}
