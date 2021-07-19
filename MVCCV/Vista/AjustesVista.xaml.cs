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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para AjustesVista.xaml
    /// </summary>
    public partial class AjustesVista : UserControl
    {
        
        private bool Admin;
        private String Nombree;
        public AjustesVista()
        {
            InitializeComponent();
        }

        public AjustesVista(bool admin, string name)
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
     
        }

        private void btnEditarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
      
        }

        private void btnAgregarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {
    
        }
    }
}
