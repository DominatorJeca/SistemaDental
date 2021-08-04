using System;
using System.Windows;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        private bool Admin;
        private String Nombree;
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
            /* Menu menu = new Menu(Admin, Nombree);
             menu.Show();
             this.Hide();*/
        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            EditarUsuario editarUsuario = new EditarUsuario(Admin, Nombree);
            editarUsuario.Show();
            this.Hide();
        }

        private void btnAgregarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
