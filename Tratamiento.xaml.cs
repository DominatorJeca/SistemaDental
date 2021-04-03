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
    /// Interaction logic for Tratamiento.xaml
    /// </summary>
    public partial class Tratamiento : Window
    {
        ClaseTratamiento tratamiento = new ClaseTratamiento();
        public Tratamiento()
        {
            InitializeComponent();
            MostrarTratamientos();
            MostrarPacientes();
        }

        public void MostrarTratamientos()
        {
            cmbTratamiento.ItemsSource = tratamiento.mostrarTratamientos();
            cmbTratamiento.DisplayMemberPath = "NombreTratamiento";
            cmbTratamiento.SelectedValuePath = "IdTratamiento";
        }

        public void MostrarPacientes()
        {
            cmbPaciente.ItemsSource = tratamiento.mostrarIdPacientes();
            cmbPaciente.DisplayMemberPath = "IdPaciente";
            cmbPaciente.SelectedValuePath = "IdPaciente";
        }

        public void MostrarMateriales()
        {
            dg_materiales.ItemsSource = tratamiento.mostrarMateriales(Convert.ToInt32(cmbTratamiento.SelectedValue));
        }

        private void cmbTratamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarMateriales();
        }
    }
}
