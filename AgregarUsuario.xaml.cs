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
        private Puesto puesto = new Puesto();
        public AgregarUsuario()
        {
            InitializeComponent();
            MostrarPuesto();
           
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }

        public void MostrarPuesto()
        {
            cmbPuesto.ItemsSource = puesto.MostrarPuestos();
            cmbPuesto.SelectedValuePath = "Id";
            cmbPuesto.DisplayMemberPath = "NombrePuesto";
        }

        public void ObtenerValores()
        {
            usuario.Id = Convert.ToString(txtAgregarIdentidad.Text);
            usuario.Nombre = Convert.ToString(txtAgregarNombre.Text);
            usuario.Apellido = Convert.ToString(txtAgregarApellido.Text);
            usuario.Telefono = Convert.ToString(txtAgregarTelefono.Text);
            usuario.Correo = Convert.ToString(txtAgregarCorreo.Text);
            usuario.Puesto = Convert.ToInt32(cmbPuesto.SelectedValue);
            usuario.Genero = ((ComboBoxItem)cmbSexo.SelectedItem).Content.ToString();
            usuario.Contraseña = Convert.ToString(txtAgregarContra.Text);
            usuario.Estado = true;
            usuario.Administrador = false;
        }


        /// <summary>
        /// Verifica que los valores de los textbox y combobox no esten vacios
        /// </summary>
        /// <returns>Verificacion de valores</returns>
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
            else if (txtConfirmarContra.Text != txtAgregarContra.Text)
            {
                MessageBox.Show("La confirmación de contraseña no coincide");
                return false;
            }
            else if (txtConfirmarCorreo.Text != txtAgregarCorreo.Text)
            {
                MessageBox.Show("La confirmaciónn de correo no coincide");
                return false;
            }


            return true;
        }

        /// <summary>
        /// Funcion para limpiar los textbox y combobox del formulario
        /// </summary>
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

        private void btnAgregarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores() == true)
            {
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario 
                    usuario.IngresarUsuario(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos insertados correctamente!");
                    LimpiarFormulario();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar el empleado...");
                    Console.WriteLine(ex.Message);
                }
              
            }

        }

    }
}
