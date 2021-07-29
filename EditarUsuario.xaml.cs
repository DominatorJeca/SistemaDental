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
using System.Text.RegularExpressions;

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
      

        Validaciones validar = new Validaciones();

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
            usuario.Genero = 1;
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
            else if (!validar.ValidarEmail(txtEditarCorreo.Text) )
            {
                MessageBox.Show("Por favor, ingrese un correo valido");
                return false;
            }
            else if (cmbPuesto.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el puesto del empleado");
                return false;
            }



            return true;
        }

        private bool VerificarCampos()
        {
            bool band = true;
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                switch (tb.Name)
                {
                    case "txtEditarTelefono":
                        {
                            if (int.Parse(tb.Text) <= 100000000 && int.Parse(tb.Text) >= 99999999)
                            {
                                band = false;
                                MessageBox.Show("El numero de telefono no es valido");
                            }
                               
                            break;
                        }

                  
                    default:
                        {
                            if (tb.Text.Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox") { 
                                band = false;
                            MessageBox.Show("Un campo no contiene el formato correcto");
                            }
                            break;
                        }
                }
                

            }

            return band;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
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
            if (VerificarValores() == true && VerificarCampos() == true)
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
            

            validar.SoloLetras(e);

        }


        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
           

            validar.SoloNumeros(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
        }

      
    }
}
