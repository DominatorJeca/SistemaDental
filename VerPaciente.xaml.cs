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
    /// Interaction logic for VerPaciente.xaml
    /// </summary>
    public partial class VerPaciente : Window
    {

        private ClasePaciente unPaciente = new ClasePaciente();
        public VerPaciente()
        {
            InitializeComponent();
            MostrarPacientes();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            pacientes.Show();
        }

        public void MostrarPacientes()
        {
            cmbPaciente.ItemsSource = unPaciente.MostrarPacientes();
            cmbPaciente.SelectedValuePath = "id_paciente";
            cmbPaciente.DisplayMemberPath = "id_paciente";
        }

        private void btnEditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbPaciente.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un paciente.");
                }
            
            }
            catch
            { }
        }
    }
}
