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
    /// Interaction logic for EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        private Usuario usuario = new Usuario();
        private Puesto puesto = new Puesto();
        public EditarUsuario()
        {
            InitializeComponent();
            MostrarUsuario();
            MostrarPuesto();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }
        public void MostrarPuesto()
        {
            cmbPuesto.ItemsSource = puesto.MostrarPuestos();
            cmbPuesto.SelectedValuePath = "Id";
            cmbPuesto.DisplayMemberPath = "NombrePuesto";
        }

        public void MostrarUsuario()
        {
            cmbUsuario.ItemsSource = usuario.MostrarUsuarios();
            cmbUsuario.SelectedValuePath = "Id";
            cmbUsuario.DisplayMemberPath = "Id";
        }

        

        private void cmbUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            cmbPuesto.Text = puesto.MostrarPuesto(Convert.ToInt32(cmbPuesto.Text));
        }
    }
}
