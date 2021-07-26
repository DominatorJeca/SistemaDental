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
using System.Net;
using System.Net.Mail;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Interaction logic for ReestablecerContrasenia.xaml
    /// </summary>
    public partial class ReestablecerContrasenia : Window
    {
        Usuario user = new Usuario();
        string codigoRec = null;
        ClaseProcedimiento proc = new ClaseProcedimiento();
        bool[] btns = { true, true};
        
        public ReestablecerContrasenia()
        {
            InitializeComponent();
        }

        private void btnBuscarCorreo_Click(object sender, RoutedEventArgs e)
        {
        
             codigoRec= proc.EnviarCodigoRecuperacion(txtCorreo.Text);
                pnlPaso1.Visibility = Visibility.Collapsed;
                pnlPaso2.Visibility = Visibility.Visible;

        }

    

       

        private void btnview3_Click(object sender, RoutedEventArgs e)
        {

            if (btns[0])
            {
                btnview3.Content = FindResource("ver");
                pswNuevaContra.Visibility = Visibility.Collapsed;
                txtNuevaContra.Visibility = Visibility.Visible;
                txtNuevaContra.Text = pswNuevaContra.Password;

                btns[0] = false;
            }
            else
            {
                btnview3.Content = FindResource("Nover");
                pswNuevaContra.Visibility = Visibility.Visible;
                txtNuevaContra.Visibility = Visibility.Collapsed;
                pswNuevaContra.Password = txtNuevaContra.Text;
                btns[0] = true;

            }
        }

        private void btnview4_Click(object sender, RoutedEventArgs e)
        {
            if (btns[1])
            {
                btnview4.Content = FindResource("ver");
                pswConfNuevaContra.Visibility = Visibility.Collapsed;
                txtConfNuevaContra.Visibility = Visibility.Visible;
                txtConfNuevaContra.Text = pswConfNuevaContra.Password;

                btns[1] = false;
            }
            else
            {
                btnview4.Content = FindResource("Nover");
                pswNuevaContra.Visibility = Visibility.Visible;
                txtNuevaContra.Visibility = Visibility.Collapsed;
                pswNuevaContra.Password = txtNuevaContra.Text;
                btns[1] = true;

            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          /*  Login log = new Login();
            log.Show();*/
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnVerificarCodigo_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigoVer.Text == codigoRec)
            {
                pnlPaso2.Visibility = Visibility.Collapsed;
                pnlPaso3.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("El Codigo es incorrecto porfavor intente de nuevo");
            }
        }

        private void btnCambiarContr_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox[] psw = {  pswNuevaContra, pswConfNuevaContra };
            string[] txt = { txtNuevaContra.Text, txtConfNuevaContra.Text};
            for (int i=0; i<2;i++)
            {
                if (btns[i])
                    txt[i] = psw[i].Password;
            }
            if (txt[0] == txt[1])
            {

                user = proc.ModificarUsuario(contra: txt[0], contraCambio: txt[0], correo: txtCorreo.Text);
                if (user !=null)
                {
                    MessageBox.Show("Su contraseña ha sido cambiada exitosamente");
                    Menu menu = new Menu(user.Administrador, user.Nombre, user.Ide);
                    this.Close();
                    menu.Show();

                }
          
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden verifique de vuelta");
            }
        }
    }
}
