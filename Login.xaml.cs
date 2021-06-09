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
        private Usuario usuario = new Usuario();
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //buscar usuario
                Usuario elUsuario = usuario.BuscarUsuario(txtUsuario.Text);


                //Verificar si el usuario existe
                if (elUsuario.Id == null)
                {
                    MessageBox.Show("El usuario o contraseña es incorrecto");
                }
                else
                {
                    if ((elUsuario.Contraseña == txtPassword.Password) && (elUsuario.Estado == true))
                    {
                        //Abrir formulario Menu
                        Menu menu = new Menu(elUsuario.Administrador,elUsuario.Nombre);
                        menu.Show();
                        this.Hide();
                    }
                    else if (!elUsuario.Estado)
                    {
                        MessageBox.Show("Su usuario se encuentra deshabilitado, por favor comunicarse con ");
                    }
                    else
                    {
                        MessageBox.Show("El usuario o contraseña es incorrecto");
                    }
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
    }
}
