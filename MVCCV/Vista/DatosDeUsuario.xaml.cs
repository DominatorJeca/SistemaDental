using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para DatosDeUsuario.xaml
    /// </summary>
    public partial class DatosDeUsuario : UserControl
    {
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        public DatosDeUsuario()
        {
            InitializeComponent();
            var Usuario = procedimiento.DatosUsuarios("javi");
            txtApellido.Text = Usuario.Apellido;
            txtNombre.Text = Usuario.Nombre;
            txtCorreo.Text = Usuario.Correo;
            txtTelefono.Text = Usuario.Telefono;
            txtsexo.Text = Usuario.Genero;
            txtpuesto.Text = Usuario.PuestoNombre;
        }

        
    }
}
