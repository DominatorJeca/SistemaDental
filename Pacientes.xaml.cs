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
    /// Interaction logic for Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window
    {
        public Pacientes()
        {
            InitializeComponent();
        }

        private void btnVerPaciente_Click(object sender, RoutedEventArgs e)
        {
            VerPaciente verpaciente = new VerPaciente();
            verpaciente.Show();
            this.Close();
        }

        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            AgregarPaciente agregarPaciente = new AgregarPaciente();
            agregarPaciente.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
