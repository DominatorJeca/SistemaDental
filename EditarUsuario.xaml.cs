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
    /// Interaction logic for EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        //variable miembro
        private Usuario usuario = new Usuario();
        private Puesto puesto = new Puesto();
        private bool Admin;
        private String Nombree;

        //constructores
        public EditarUsuario()
        {
            InitializeComponent();
            MostrarUsuario();
            MostrarPuesto();

        }

        public EditarUsuario(bool admin, string name)
        {
            InitializeComponent();
            MostrarUsuario();
            MostrarPuesto();
            Nombree = name;
            Admin = admin;

        }

        //Abre el formulario ajustes y cierra el actual
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
            this.Hide();
        }
        //Asigna la lista de puestos al combobox de puestos
        public void MostrarPuesto()
        {
            cmbPuesto.ItemsSource = puesto.MostrarPuestos();
            cmbPuesto.SelectedValuePath = "Id";
            cmbPuesto.DisplayMemberPath = "NombrePuesto";
        }

        //Asigna la lista de usuario a el combobox de usuarios
        public void MostrarUsuario()
        {
            cmbUsuario.ItemsSource = usuario.MostrarUsuarios();
            cmbUsuario.SelectedValuePath = "Id";
            cmbUsuario.DisplayMemberPath = "Id";
        }

        /// <summary>
        /// Obtiene los valores de los textbox y combobox
        /// </summary>
        public void ObtenerValores()
        {
            usuario.Id = Convert.ToString(cmbUsuario.SelectedValue);
            usuario.Nombre = Convert.ToString(txtEditarNombre.Text);
            usuario.Apellido = Convert.ToString(txtEditarApellido.Text);
            usuario.Telefono = Convert.ToString(txtEditarTelefono.Text);
            usuario.Correo = Convert.ToString(txtEditarCorreo.Text);
            usuario.Puesto = Convert.ToInt32(cmbPuesto.SelectedValue);
            usuario.Genero = ((ComboBoxItem)cmbSexo.SelectedItem).Content.ToString();
            usuario.Contraseña = Convert.ToString(txtNuevaContra.Text);
            usuario.Estado = true;
            usuario.Administrador = false;
        }


        /// <summary>
        /// Verifica que los valores de los textbox y combobox no esten vacios
        /// </summary>
        /// <returns>Verificacion de valores</returns>
        private bool VerificarValores()
        {
            if (txtEditarApellido.Text == string.Empty || txtEditarNombre.Text == string.Empty || txtEditarCorreo.Text == string.Empty
                 || txtEditarTelefono.Text == string.Empty || txtNuevaContra.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbSexo.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Sexo del empleado");
                return false;
            }
            else if (cmbPuesto.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el puesto del empleado");
                return false;
            }

            return true;
        }


        /// <summary>
        /// Funcion para limpiar los textbox y combobox del formulario
        /// </summary>
        private void LimpiarFormulario()
        {
            txtEditarApellido.Text = string.Empty;
            txtEditarNombre.Text = string.Empty;
            txtEditarCorreo.Text = string.Empty;

            txtEditarTelefono.Text = string.Empty;
            txtNuevaContra.Text = string.Empty;
            cmbUsuario.SelectedValue = null;
            cmbPuesto.SelectedValue = null;
            cmbSexo.SelectedValue = null;

        }

        /// <summary>
        /// Llama al metodo para actualizar usuario de la clase usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores() == true)
            {
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario 
                    usuario.EditarUsuario(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos editados correctamente!");
                    LimpiarFormulario();
                    MostrarUsuario();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de editar el empleado...");
                    Console.WriteLine(ex.Message);
                }

            }

        }

        /// <summary>
        /// Devuelve el formulario a su forma inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            MostrarUsuario();

        }

        /// <summary>
        /// Abre el formulario ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
        }


        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;

        }
    }
}
