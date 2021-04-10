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
            usuario.Id = Convert.ToString(txtAgregarIdentidad.Text);
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

        private bool VerificarValores()
        {
            if (txtAgregarApellido.Text == string.Empty || txtAgregarNombre.Text == string.Empty || txtAgregarCorreo.Text == string.Empty
                || txtAgregarIdentidad.Text == string.Empty || txtAgregarTelefono.Text == string.Empty || txtAgregarContra.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbSexo.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Sexo del nuevo empleado");
                return false;
            }
            else if (cmbPuesto.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el puesto del nuevo empleado");
                return false;
            }
            else if (txtConfirmarContra != txtAgregarContra)
            {
                MessageBox.Show("La confirmaciónn de contraseña no coincide");
                return false;
            }
            else if (txtConfirmarCorreo != txtAgregarContra)
            {
                MessageBox.Show("La confirmaciónn de correo no coincide");
                return false;
            }


            return true;
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores())
            {
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario 
                    usuario.IngresarUsuario(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos insertados correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar el empleado...");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarFormulario();
                }
            }

        }

        private void LimpiarFormulario()
        {
            txtAgregarApellido.Text = string.Empty;
            txtAgregarNombre.Text = string.Empty;
            txtAgregarCorreo.Text = string.Empty;
            txtAgregarIdentidad.Text = string.Empty; 
            txtAgregarTelefono.Text= string.Empty;
            txtAgregarContra.Text = string.Empty;
            txtConfirmarContra.Text = string.Empty;
            txtConfirmarCorreo.Text = string.Empty;
            cmbPuesto.SelectedValue = null;
            cmbSexo.SelectedValue = null;

        }
    }
}
