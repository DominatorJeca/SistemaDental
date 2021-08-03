using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para DatosDeUsuario.xaml
    /// </summary>
    public partial class DatosDeUsuario : UserControl
    {
        private string nombreusaurio;
        private int iduser;
        private int edicion = 0;
        private ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        private Usuario usuarios = new Usuario();
        private Validaciones validar = new Validaciones();
        public DatosDeUsuario()
        {
            InitializeComponent();
            LlenadoDeInformacion();
            dg_citasdia.ItemsSource = procedimiento.CitasUsuario("JCASTRO1");
        }

        public DatosDeUsuario(string usuario, int idusuario)
        {
            InitializeComponent();
            nombreusaurio = usuario;
            iduser = idusuario;
            LlenadoDeInformacion();
            dg_citasdia.ItemsSource = procedimiento.CitasUsuario(usuario);

        }

        private void btnActualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotones(true, Visibility.Visible);
            btnActualizarUsuario.Visibility = Visibility.Collapsed;
            btnActualizarContraseña.Visibility = Visibility.Visible;
            txtcontras(false);
        }

        private void HabilitarBotones(bool mostrar, Visibility visibility)
        {
            txtApellido.IsEnabled = mostrar;
            txtNombre.IsEnabled = mostrar;
            txtCorreo.IsEnabled = mostrar;
            txtTelefono.IsEnabled = mostrar;
            btnActualizarContraseña.IsEnabled = mostrar;


            btnActualizarContraseña.Visibility = visibility;
            txtContraseniaActual.Visibility = visibility;
            txtNuevaContra.Visibility = visibility;
            txtNuevaContra_Copy.Visibility = visibility;
            btnguardar.Visibility = visibility;
            btnCancelar.Visibility = visibility;
        }

        private void ObtenerValores()
        {
            usuarios.usuario = nombreusaurio;
            usuarios.Nombre = txtNombre.Text;
            usuarios.Apellido = txtApellido.Text;
            usuarios.Telefono = txtTelefono.Text;
            usuarios.Correo = txtCorreo.Text;
            usuarios.Contraseña = txtContraseniaActual.Password;
            if (txtNuevaContra.Password == txtNuevaContra_Copy.Password)
            {
                usuarios.Contrasenianueva = txtNuevaContra.Password;
            }
        }
        private void btnguardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario elusuario = procedimiento.BuscarUsuario(nombreusaurio, txtContraseniaActual.Password);
            if (edicion == 0)
            {
                if (validar.VerificarCampos(this) && validar.ValidarEmail(txtCorreo.Text) && validar.VerificarNumero(txtTelefono.Text) && elusuario != null)
                {
                    ObtenerValores();
                    procedimiento.EditarUsuarioSinPass(usuarios);
                    procedimiento.InsertarLog(iduser, "Se editó información acerca de su cuenta personal");
                    HabilitarBotones(false, Visibility.Collapsed);
                    btnActualizarUsuario.Visibility = Visibility.Visible;
                    LlenadoDeInformacion();
                    LimpiarPass();
                    txtcontras(false);
                }
                else if (!validar.ValidarEmail(txtCorreo.Text))
                {
                    MessageBox.Show("El correo que intenta ingresar no es válido");
                }
                else if (!validar.VerificarNumero(txtTelefono.Text))
                {
                    MessageBox.Show("Su número telefónico no es correcto");
                }
                else
                {
                    MessageBox.Show("Asegurese de verificar la integridad de sus datos");
                }
            }
            else
            {
                if (validar.VerificarCampos(this) && validar.ValidarEmail(txtCorreo.Text) && validar.VerificarNumero(txtTelefono.Text) && verificarPass() && elusuario != null)
                {
                    ObtenerValores();
                    procedimiento.EditarUsuario(usuarios);
                    procedimiento.InsertarLog(iduser, "Se editó información acerca de su cuenta personal y su contraseña fue cambiada");
                    HabilitarBotones(false, Visibility.Collapsed);
                    btnActualizarUsuario.Visibility = Visibility.Visible;
                    LlenadoDeInformacion();
                    LimpiarPass();
                    txtcontras(false);
                    edicion = 0;
                }
                else if (!validar.ValidarEmail(txtCorreo.Text))
                {
                    MessageBox.Show("El correo que intenta ingresar no es válido");
                }
                else if (!validar.VerificarNumero(txtTelefono.Text))
                {
                    MessageBox.Show("Su número telefónico no es correcto");
                }
                else
                {
                    MessageBox.Show("Asegurese de verificar la integridad de sus datos");
                }
            }
        }

        private void LlenadoDeInformacion()
        {
            var Usuario = procedimiento.DatosUsuarios(nombreusaurio);
            txtApellido.Text = Usuario.Apellido;
            txtNombre.Text = Usuario.Nombre;
            txtCorreo.Text = Usuario.Correo;
            txtTelefono.Text = Usuario.Telefono;
            txtpuesto.Text = Usuario.PuestoNombre;
        }

        private bool verificarPass()
        {
            if (!validar.verificarpass(this))
            {
                MessageBox.Show("Por favor ingrese los valores necesarios");
                return false;
            }
            else if (txtContraseniaActual.Password.Length < 8 || (txtNuevaContra.Password.Length < 8 && edicion == 1))
            {
                MessageBox.Show("La longitud de la contraseña debe de ser como mínimo de 8 caracteres");
                return false;
            }
            return true;
        }
        private void LimpiarPass()
        {
            txtNuevaContra.Password = "";
            txtNuevaContra_Copy.Password = "";
            txtContraseniaActual.Password = "";
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnActualizarUsuario.Visibility = Visibility.Visible;
            HabilitarBotones(false, Visibility.Collapsed);
            LlenadoDeInformacion();
            txtcontras(false);
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
            txtcontras(true);
            btnActualizarContraseña.IsEnabled = false;
        }

        private void txtcontras(bool show)
        {
            txtNuevaContra.IsEnabled = show;
            txtNuevaContra_Copy.IsEnabled = show;
        }
    }
}
