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
        public Ajustes()
        {
            InitializeComponent();
        }

        private void btnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            EditarUsuario editarUsuario = new EditarUsuario();
            editarUsuario.Show();
        }

        private void btnAgregarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            AgregarUsuario agregarUsuario = new AgregarUsuario();
            agregarUsuario.Show();
        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejarUsuario manejarUsuario = new ManejarUsuario();
            manejarUsuario.Show();
        }
    }
}
