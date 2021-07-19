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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        //private Usuario usuario = new Usuario();
        private ClaseProcedimiento proced = new ClaseProcedimiento();
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Hidden)
            {
                txtPassword.Password = txtpas.Text;
            }


            try
            {
                //buscar usuario
                Usuario elUsuario = proced.BuscarUsuario(txtUsuario.Text, txtPassword.Password);
                

                //Verificar si el usuario existe
                if (elUsuario == null)
                {
                    MessageBox.Show("El usuario o contraseña es incorrecto");
                }
                else
                { 
                        //Abrir formulario Menu
                        EditarUsuario menu = new EditarUsuario();
                        menu.Show();
                        this.Hide();
                    
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error al momento de realizar la consulta");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            
                txtPassword.Visibility = Visibility.Hidden;
                txtpas.Visibility = Visibility.Visible;
                txtpas.Text = txtPassword.Password;
                btnview.Visibility=Visibility.Hidden;
                btnview2.Visibility = Visibility.Visible;

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
            btnview2.Visibility = Visibility.Hidden;
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
    }
}
