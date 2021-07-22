using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para VistaAgregarUsuario.xaml
    /// </summary>
    public partial class VistaAgregarUsuario : UserControl
    {

        //Variables miembro
        private Usuario usuario = new Usuario();
        int opcion = 0;
        private bool Admin;
        private String Nombree;
        private ClaseProcedimiento Proc = new ClaseProcedimiento();
        Validaciones validar = new Validaciones();

        //Contructores
        public VistaAgregarUsuario()
        {
            InitializeComponent();
            MostrarPuesto();
            MostrarMaterial();

        }

        public VistaAgregarUsuario(bool admin, string name)
        {
            InitializeComponent();
            MostrarPuesto();
            MostrarMaterial();
            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Abre el formulario ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Asigna la lista de puestos al cmbPuestos
        /// </summary>
        public void MostrarPuesto()
        {
            cmbPuesto.ItemsSource = Proc.MostrarPuestos();
            cmbPuesto.SelectedValuePath = "Id";
            cmbPuesto.DisplayMemberPath = "NombrePuesto";
        }

        /// <summary>
        /// Obtiene los datos de los textbox y combobox en el formulario
        /// </summary>
        public void ObtenerValores()
        {
            usuario.Id = Convert.ToString(txtAgregarIdentidad.Text);
            usuario.Nombre = Convert.ToString(txtAgregarNombre.Text);
            usuario.Apellido = Convert.ToString(txtAgregarApellido.Text);
            usuario.Telefono = Convert.ToString(txtAgregarTelefono.Text);
            usuario.Correo = Convert.ToString(txtAgregarCorreo.Text);
            usuario.Puesto = Convert.ToInt32(cmbPuesto.SelectedValue);
            usuario.Genero = ((ComboBoxItem)cmbSexo.SelectedItem).Content.ToString();
            usuario.Contraseña = Convert.ToString(txtAgregarContra.Password);
            usuario.Estado = true;
            usuario.Administrador = Convert.ToBoolean(chkAdmin);
        }


        /// <summary>
        /// Verifica que los valores de los textbox y combobox no esten vacios
        /// </summary>
        /// <returns>Verificacion de valores</returns>
        private bool VerificarValores()
        {
            if (!validar.VerificarCampos(this))
            {
                MessageBox.Show("Por favor ingresa todos los valores que se le solicitan");
                return false;
            }
            else if (!validar.VerificarIdentidad(txtAgregarIdentidad.Text))
            {
                MessageBox.Show("El numero de identidad no tiene un formato correcto");
                return false;
            }
            else if (!validar.VerificarNumero(txtAgregarTelefono.Text))
            {
                MessageBox.Show("El numero de telefono no tiene un formato correcto");
                return false;
            }
            else if (txtConfirmarContra.Password != txtAgregarContra.Password)
            {
                MessageBox.Show("La confirmación de contraseña no coincide");
                return false;
            }
            else if (!validar.ValidarEmail(txtAgregarCorreo.Text))
            {
                MessageBox.Show("Por favor, ingrese un correo valido");
                return false;
            }

            return true;
        }

        public void MostrarMaterial()
        {
            dgvEmpleado.ItemsSource = Proc.MostrarEmpleadosActivos();
            dgvEmpleado.SelectedValuePath = "Id";
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
            txtAgregarTelefono.Text = string.Empty;
            txtAgregarContra.Password = string.Empty;
            txtConfirmarContra.Password = string.Empty;
            cmbPuesto.SelectedValue = null;
            cmbSexo.SelectedValue = null;

        }

        /// <summary>
        /// Lllama al metodo de agregar usuario y envia los parametros correspondientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            opcion = 1;
            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnAgregarUsuario.IsEnabled = false;


            // Verificar que se ingresaron los valores requeridos
            /* if (VerificarValores() == true)
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

             }*/

        }




        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {


            validar.SoloNumeros(e);
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {


            validar.SoloLetras(e);

        }


        private void chkAdmin_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            opcion = 2;
            btnAgregarUsuario.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnEditar.IsEnabled = false;
            btnPermisos.Visibility = Visibility.Visible;
        }

        private void btnEliminarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            opcion = 2;
            btnAgregarUsuario.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnEditar.IsEnabled = false;
            btnPermisos.Visibility = Visibility.Visible;
        }

        private void btnGuardarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            switch (opcion)
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
            }

        }

        private void IngresarEmpleado(Usuario usu)
        {
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

