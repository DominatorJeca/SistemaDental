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
        bool Admin;
        string Nombree;
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
            Inventario inventario = new Inventario();
            inventario.Show();
            this.Hide();
        }

        private void btnTratamiento_Click(object sender, RoutedEventArgs e)
        {
            Tratamiento tratamiento = new Tratamiento();
            tratamiento.Show();
            this.Hide();
        }

        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
            Caja caja = new Caja();
            caja.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            pacientes.Show();
            this.Hide();
        }

        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes(Admin,Nombree);
            this.Hide();
            ajustes.Show();
            
        }
    }
}
