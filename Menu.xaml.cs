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
       private bool Admin;
        private String Nombree;

        
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
        public void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario inventario = new Inventario(Admin,Nombree);
            inventario.Show();
            this.Hide();
        }

        private void btnTratamiento_Click(object sender, RoutedEventArgs e)
        {
            Tratamiento tratamiento = new Tratamiento(Admin, Nombree);
            tratamiento.Show();
            this.Hide();
        }

        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
            Caja caja = new Caja(Admin, Nombree);
            caja.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes(Admin, Nombree);
            pacientes.Show();
            this.Hide();
        }

        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            Citas citas = new Citas(Admin, Nombree);
            citas.Show();
            this.Hide();
        }
    }
}
