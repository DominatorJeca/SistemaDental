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
using System.Windows.Shapes;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for AgregarUsuario.xaml
    /// </summary>
    public partial class AgregarUsuario : Window
    {
        private Usuario usuario = new Usuario();
        public AgregarUsuario()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }

        public void ObtenerValores()
        {
            usuario.Id=Convert.ToString(txtAgregarIdentidad.Text);
            usuario.Nombre = Convert.ToString(txtAgregarNombre.Text);
            usuario.Apellido = Convert.ToString(txtAgregarApellido.Text);
            usuario.Telefono = Convert.ToString(txtAgregarTelefono.Text);
            usuario.Correo = Convert.ToString(txtAgregarCorreo.Text);
            usuario.Puesto = Convert.ToInt32(cmbPuesto.SelectedValue);
            usuario.Genero = Convert.ToString(cmbSexo.SelectedValue);
            usuario.Contraseña = Convert.ToString(txtAgregarContra.Text);
            usuario.Estado = true;
            usuario.Administrador = false;
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
          

      
        }
    }
}
