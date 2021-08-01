using SistemaDental.MVCCV.Vista;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Usuario usuario = new Usuario();
        ClaseProcedimiento proc = new ClaseProcedimiento();
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Collapsed)
            {
                txtPassword.Password = txtpas.Text;
            }
            try
            {
                //buscar usuario
                Usuario elUsuario = proc.BuscarUsuario(txtUsuario.Text,txtPassword.Password);
                if (elUsuario != null)
                {
                    Menu men = new Menu(elUsuario.Administrador, elUsuario.Nombre,elUsuario.Ide, elUsuario.usuario);
                    men.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Usuario o contraseña incorrectos");

                //Verificar si el usuario existe

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error al momento de realizar la consulta" + ex);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            App.Current.Shutdown();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (btnview.Content == FindResource("Nover"))
            {
                btnview.Content = FindResource("ver");
                txtPassword.Visibility = Visibility.Collapsed;
                txtpas.Visibility = Visibility.Visible;
                txtpas.Text = txtPassword.Password;
            }
            else
            {
                btnview.Content = FindResource("Nover");
                txtPassword.Visibility = Visibility.Visible;
                txtpas.Visibility = Visibility.Collapsed;
                txtPassword.Password = txtpas.Text;

            }


        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            txtPassword.Visibility = Visibility.Visible;
            txtpas.Visibility = Visibility.Hidden;
            txtPassword.Password = txtpas.Text ;
            btnview.Visibility = Visibility.Visible;
           // btnview2.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnOlvidarPass_Click(object sender, RoutedEventArgs e)
        {
            ReestablecerContrasenia reestablecer = new ReestablecerContrasenia();
                reestablecer.ShowDialog();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost/ProjectoClinicaDental/vistas/";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}
