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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
       //Variables miembros
        private bool Admin;
        private String Nombree;

        //Constructores
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(bool admin,string name)
        {
           
            InitializeComponent();
            lblUsuario.Content = name;
            PermisosAdministrador(admin);
            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Verifica si el usuario que ingreso tiene permisos de administrador
        /// </summary>
        /// <param name="admin"></param>
        public void PermisosAdministrador(bool admin)
        {
            if (admin)
            {
                btnAjustes.IsEnabled = true;
            }
            else
            {
                btnAjustes.IsEnabled = false;
            }

        }

        /// <summary>
        /// Abre el Formulario de inventario y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario inventario = new Inventario(Admin,Nombree);
            inventario.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el Formulario de tratamiento y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTratamiento_Click(object sender, RoutedEventArgs e)
        {
            Tratamiento tratamiento = new Tratamiento(Admin, Nombree);
            tratamiento.Show();
            this.Hide();
        }
        /// <summary>
        /// Abre el Formulario de caja y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
            Caja caja = new Caja(Admin, Nombree);
            caja.Show();
            this.Hide();
        }
        /// <summary>
        /// Abre el Formulario de pacientes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes(Admin, Nombree);
            pacientes.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el Formulario de ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Abre el Formulario de Citas y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            Citas citas = new Citas(Admin, Nombree);
            citas.Show();
            this.Hide();
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
