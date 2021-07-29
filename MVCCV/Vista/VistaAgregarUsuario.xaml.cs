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
using System.ComponentModel;

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
            MostrarEmpleados(true);
            MostrarGenero();
            HabilitarInhabilitarTXT(false);
            botoneshabilitados(true);

        }

        public VistaAgregarUsuario(bool admin, string name)
        {
            InitializeComponent();
            MostrarPuesto();
            MostrarGenero();

            MostrarEmpleados(true);
            botoneshabilitados(true);
            Nombree = name;
            Admin = admin;
            HabilitarInhabilitarTXT(false);
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

        public void MostrarGenero()
        {
            cmbSexo.ItemsSource = Proc.MostrarGenero();
            cmbSexo.SelectedValuePath = "Id";
             cmbSexo.DisplayMemberPath = "NombreGenero";
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
            usuario.Genero = Convert.ToInt32(cmbSexo.SelectedValue); 
          // usuario.GeneroNombre = ((ComboBoxItem)cmbSexo.SelectedItem).Content.ToString();
            usuario.Contraseña = txtAgregarContra.Password;
            usuario.Estado = true;
            usuario.Ide = (Convert.ToInt32(dgvEmpleado.SelectedValue));



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
            
                botoneshabilitados(false);
                return false;
            }
            if (!validar.VerificarIdentidad(txtAgregarIdentidad.Text))
            {
                MessageBox.Show("El numero de identidad no tiene un formato correcto");
              
                botoneshabilitados(false);
                return false;
            }
            else if (!validar.VerificarNumero(txtAgregarTelefono.Text))
            {
                MessageBox.Show("El numero de telefono no tiene un formato correcto");
            
                botoneshabilitados(false);
                return false;
            }
            else if (txtConfirmarContra.Password != txtAgregarContra.Password)
            {
                MessageBox.Show("La confirmación de contraseña no coincide");
               
                botoneshabilitados(false);
                return false;
            }
            else if (!validar.ValidarEmail(txtAgregarCorreo.Text))
            {
               
                MessageBox.Show("Por favor, ingrese un correo valido");
                botoneshabilitados(false);
                return false;
            }

            return true;
        }
        ICollectionView ColectionEmpleados;
        public void MostrarEmpleados(bool act)
        {
            ColectionEmpleados = new CollectionViewSource() { Source = Proc.MostrarEmpleados(act) }.View;
            dgvEmpleado.ItemsSource = ColectionEmpleados;
            dgvEmpleado.SelectedValuePath = "Ide";
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
            botoneshabilitados(false);
            HabilitarInhabilitarTXT(true);
        

            dgvEmpleado.IsEnabled = false;


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
            usuario.Administrador = true;
        }

        private void chkAdmin_Unchecked(object sender, RoutedEventArgs e)
        {
            usuario.Administrador = false;
        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            opcion = 2;
            botoneshabilitados(false);
            HabilitarInhabilitarTXT(true);
        }


        private void btnEliminarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            opcion = 3;
            botoneshabilitados(false);



        }

        private void botoneshabilitados(bool grupo)
        {
            if (grupo)
            {
                btnAgregarUsuario.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnEditar.IsEnabled = true;
                btnRestablecer.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAgregarUsuario.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnEditar.IsEnabled = false;
                btnRestablecer.Visibility = Visibility.Hidden;
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
            }
        }

        private void HabilitarInhabilitarTXT(bool habilitar)
        {
            txtAgregarNombre.IsEnabled = habilitar;
            txtAgregarIdentidad.IsEnabled= habilitar;
            txtAgregarApellido.IsEnabled= habilitar;
            txtAgregarContra.IsEnabled = habilitar;
            txtAgregarCorreo.IsEnabled= habilitar;
            txtConfirmarContra.IsEnabled= habilitar;
            txtAgregarTelefono.IsEnabled= habilitar;
            cmbPuesto.IsEnabled= habilitar;
            cmbSexo.IsEnabled= habilitar;
            chkAdmin.IsEnabled = habilitar;

        }
        private void btnGuardarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            switch (opcion)
            {
                case 1:
                    IngresarEmpleado();

                    break;
                case 2:
                    EditarEmpleado();
                    break;
                case 3:
                    EliminarRestablecerEmpleado(false);
                    break;
                case 4:
                    EliminarRestablecerEmpleado(true);
                    break;
            }

           // HabilitarInhabilitarTXT(false);
           // botoneshabilitados(true);
            

        }

        private void IngresarEmpleado()
        {
            if (VerificarValores() == true)
            {
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario
                    Proc.IngresarUsuario(usuario);

                    // Mensaje de inserción exitosa




                    obtenerUsuario();
                    HabilitarInhabilitarTXT(false);
                    botoneshabilitados(true);
                    LimpiarFormulario();
                    dgvEmpleado.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar el empleado..."+ ex.Message);
                    Console.WriteLine(ex.Message);
                }

                MostrarEmpleados(true);
            }
        }


        private void obtenerUsuario()
        {

                try
                {

                string usu;
                // Insertar los datos del usuario
                usu = Proc.obtenerusuario(usuario.Id);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("El usuario para el empleado que ingresó es: "+ usu);

                LimpiarFormulario();
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de obtener el usuario..." + ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }

        private void EditarEmpleado()
        {
            if (VerificarValores() == true)
            {
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario
                    Proc.EditarEmpleado(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos editados correctamente!");
                    HabilitarInhabilitarTXT(false);
                    LimpiarFormulario();

                    botoneshabilitados(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de editar el empleado..." + ex.Message);
                    Console.WriteLine(ex.Message);
                }

                MostrarEmpleados(true);
            }
        }

        private void EliminarRestablecerEmpleado(bool act)
        {


                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();
                    usuario.Estado = act;
                    // Insertar los datos del usuario
                    Proc.EditarEmpleado(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("La operación se realizó con exito!");

                    LimpiarFormulario();

                botoneshabilitados(true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de eliminar el empleado..." + ex.Message);
                    Console.WriteLine(ex.Message);
                }
            MostrarEmpleados(true);
        }

        private void dgvEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            MostrarEmpleados(false);
            opcion = 4;
            botoneshabilitados(false);
            HabilitarInhabilitarTXT(false);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MostrarEmpleados(true);
            LimpiarFormulario();
            botoneshabilitados(true);
            HabilitarInhabilitarTXT(false);
            dgvEmpleado.IsEnabled = true;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtBuscarEmpleadoID.Text == "")
            {
                ColectionEmpleados.Filter = null;
                dgvEmpleado.ItemsSource = ColectionEmpleados;
            }
            else
            {
                var filtro = new Predicate<object>( item => (((Usuario)item).Id).Contains(txtBuscarEmpleadoID.Text));
                ColectionEmpleados.Filter = filtro;
                dgvEmpleado.ItemsSource = ColectionEmpleados;
            }
        }



    }
}
