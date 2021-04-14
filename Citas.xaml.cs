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
    /// Interaction logic for Citas.xaml
    /// </summary>
    public partial class Citas : Window
    {
        ClaseCitas citas = new ClaseCitas();
        public Citas()
        {
            InitializeComponent();
            MostrarDatos();
        }
        
        private void MostrarDatos()
        {
            cmbPaciente.ItemsSource = citas.mostrarIdPacientes();
            cmbPaciente.DisplayMemberPath = "IdPacientes";
            cmbPaciente.SelectedValuePath = "IdPacientes";
            cmbEmpleado.ItemsSource = citas.MostrarEmpleado();
            cmbEmpleado.SelectedValuePath = "IdDoctor";
            cmbTratamiento.ItemsSource = citas.MostrarTratamiento();
            cmbTratamiento.DisplayMemberPath = "NombreTratamiento";
            cmbTratamiento.SelectedValuePath = "IdTratamiento";
        }

        private void ObtenerValores()
        {
            citas.IdDoctor = cmbEmpleado.SelectedValue.ToString();
            citas.IdPacientes = cmbPaciente.SelectedValue.ToString();
            citas.IdTratamiento =Convert.ToInt32(cmbTratamiento.SelectedValue.ToString());
            citas.fechaCita = cdCitas.SelectedDate.Value;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbEmpleado.SelectedValue.ToString() != null && cmbPaciente.SelectedValue.ToString() != null && Convert.ToInt32(cmbTratamiento.SelectedValue) > 0)
                {
                    ObtenerValores();
                    citas.AgendarCita(citas);
                    MessageBox.Show("Cita agendada con éxito");
                }
                else
                {
                    MessageBox.Show("Debe seleccionar todos los elementos necesarios para agendar una cita y debe revisar que su fecha sea la correcta");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("La fecha no es correcta" + ex);
            }
            finally
            {

            }
        }
    }
}
