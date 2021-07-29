using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
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
        string contraseniaverificar;
        int edicion = 0;
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        Usuario usuarios = new Usuario();
        Validaciones validar = new Validaciones();
        public DatosDeUsuario()
        {
            InitializeComponent();
            LlenadoDeInformacion();
            dg_citasdia.ItemsSource = procedimiento.CitasUsuario("javi");
        }

        private void btnActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotones(true, Visibility.Visible);
            btnActualizarUsuario.Visibility = Visibility.Collapsed;

        }

        private void HabilitarBotones(bool mostrar, Visibility visibility)
        {
            txtApellido.IsEnabled = mostrar;
            txtNombre.IsEnabled =mostrar;
            txtCorreo.IsEnabled = mostrar;
            txtTelefono.IsEnabled = mostrar;


            btnActualizarContraseña.Visibility = visibility;
            txtContraseniaActual.Visibility = visibility;
            txtNuevaContra.Visibility = visibility;
            txtNuevaContra_Copy.Visibility = visibility;
            btnguardar.Visibility = visibility;
            btnCancelar.Visibility = visibility;
        }

        private void ObtenerValores()
        {
            usuarios.usuario = "javi";
            usuarios.Nombre = txtNombre.Text;
            usuarios.Apellido = txtApellido.Text;
            usuarios.Telefono = txtTelefono.Text;
            usuarios.Correo = txtCorreo.Text;
            usuarios.Contraseña = txtContraseniaActual.Password;
            if (txtNuevaContra.Password == txtNuevaContra_Copy.Password)
                usuarios.Contrasenianueva = txtNuevaContra.Password;

        }
        private void btnguardar_Click(object sender, RoutedEventArgs e)
        {
            if (edicion == 0)
            {
                if (validar.VerificarCampos(this) && validar.ValidarEmail(txtCorreo.Text) && validar.VerificarNumero(txtTelefono.Text) && txtContraseniaActual.Password == contraseniaverificar)
                {
                    ObtenerValores();
                    procedimiento.EditarUsuarioSinPass(usuarios);
                    HabilitarBotones(false, Visibility.Collapsed);
                    btnActualizarUsuario.Visibility = Visibility.Visible;
                    LlenadoDeInformacion();
                    txtNuevaContra.Password = "";
                    txtNuevaContra_Copy.Password = "";
                    txtContraseniaActual.Password = "";
                }
                else if (!validar.ValidarEmail(txtCorreo.Text))
                    MessageBox.Show("El correo que intenta ingresar no es válido");
                else if (!validar.VerificarNumero(txtTelefono.Text))
                    MessageBox.Show("Su número telefónico no es correcto");
                else
                    MessageBox.Show("Asegurese de verificar la integridad de sus datos");
            }
            else
            {
                if (validar.VerificarCampos(this) && validar.ValidarEmail(txtCorreo.Text) && validar.VerificarNumero(txtTelefono.Text) && validar.verificarpass(this) && txtContraseniaActual.Password == contraseniaverificar)
                {
                    ObtenerValores();
                    procedimiento.EditarUsuario(usuarios);
                    HabilitarBotones(false, Visibility.Collapsed);
                    btnActualizarUsuario.Visibility = Visibility.Visible;
                    LlenadoDeInformacion();
                    txtNuevaContra.Password = "";
                    txtNuevaContra_Copy.Password = "";
                    txtContraseniaActual.Password = "";
                }
                else if (!validar.ValidarEmail(txtCorreo.Text))
                    MessageBox.Show("El correo que intenta ingresar no es válido");
                else if (!validar.VerificarNumero(txtTelefono.Text))
                    MessageBox.Show("Su número telefónico no es correcto");
                else if (!validar.verificarpass(this))
                    MessageBox.Show("Corrobore la información acerca de sus contraseñas");
                else
                    MessageBox.Show("Asegurese de verificar la integridad de sus datos");
            }
        }

        private void LlenadoDeInformacion()
        {
            var Usuario = procedimiento.DatosUsuarios("javi");
            txtApellido.Text = Usuario.Apellido;
            txtNombre.Text = Usuario.Nombre;
            txtCorreo.Text = Usuario.Correo;
            txtTelefono.Text = Usuario.Telefono;
            txtpuesto.Text = Usuario.PuestoNombre;
            contraseniaverificar = Usuario.Contraseña;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnActualizarUsuario.Visibility = Visibility.Visible;
            HabilitarBotones(false, Visibility.Collapsed);
            LlenadoDeInformacion();
            txtNuevaContra.Password = "";
            txtNuevaContra_Copy.Password = "";
            txtContraseniaActual.Password = "";
        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {


            validar.SoloNumeros(e);
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {


            validar.SoloLetras(e);

        }

        private void btnActualizarContraseña_Click(object sender, RoutedEventArgs e)
        {
            edicion = 1;
            txtNuevaContra.IsEnabled = true;
            txtNuevaContra_Copy.IsEnabled = true;
            btnActualizarContraseña.IsEnabled = false;
        }
    }
}
