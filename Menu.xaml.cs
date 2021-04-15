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
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(bool admin,string name)
        {
            Admin = admin;
            InitializeComponent();
            lblUsuario.Content = name;
        }

        public void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.Show();
        }

        private void btnTratamiento_Click(object sender, RoutedEventArgs e)
        {
            Tratamiento tratamiento = new Tratamiento();
            tratamiento.Show();
        }

        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
            Caja caja = new Caja();
            caja.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            pacientes.Show();
        }

        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }
    }
}
