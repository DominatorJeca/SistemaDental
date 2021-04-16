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
    /// Interaction logic for Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        bool Admin;
        string Nombree;
        public Ajustes()
        {
            InitializeComponent();
        }

        public Ajustes(bool admin, string name)
        {
            InitializeComponent();
            Nombree = name;
            Admin = admin;
        }

        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(Admin,Nombree);
            menu.Show();
            this.Hide();
        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            EditarUsuario editarUsuario = new EditarUsuario(Admin, Nombree);
            editarUsuario.Show();
            this.Hide();
        }

        private void btnAgregarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            AgregarUsuario agregarUsuario = new AgregarUsuario(Admin, Nombree);
            agregarUsuario.Show();
            this.Hide();
        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejarUsuario manejarUsuario = new ManejarUsuario(Admin, Nombree);
            manejarUsuario.Show();
            this.Hide();
        }
    }
}
