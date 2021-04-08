using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Data;
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
        DateTime DateTime = new DateTime();
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
            dg_materiales.SelectedValuePath = "NombreMaterial";
        }

        private void cmbTratamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarMateriales();
        }

        public void ObtenerValores()
        {
            tratamiento.fecha = DateTime.Now;
            tratamiento.IdPaciente = Convert.ToString(cmbPaciente.SelectedValue);
            tratamiento.IdTratamiento = Convert.ToInt32(cmbTratamiento.SelectedValue);
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (Convert.ToInt32(cmbTratamiento.SelectedValue) > 0 && Convert.ToString(cmbPaciente.SelectedValue) != " ")
                {
                    ObtenerValores();
                    tratamiento.IngresarAlHistorial(tratamiento);
                    foreach (ClaseTratamiento tratamientos in dg_materiales.ItemsSource)
                    {
                        tratamiento.Cantidad=Convert.ToInt32(tratamientos.Cantidad.ToString());
                        tratamiento.NombreMaterial = tratamientos.NombreMaterial.ToString();
                        tratamiento.ActualizarMaterialDisponible(tratamiento);
                    }
                }
                else
                {
                    MessageBox.Show("Asegurese de seleccionar un tratamiento y un paciente");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                MessageBox.Show("Ingreso al historial fue un éxito");
                cmbPaciente.SelectedItem = null;
                cmbTratamiento.SelectedItem = null;
            }
            
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (dg_materiales.SelectedValue == null || cmbTratamiento.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un tratamiento y un material para poder ser editado");
                }
                else
                {

                    txtCantidad.IsEnabled = true;
                    btnGuardar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnRealizar.IsEnabled = false;
                    cmbTratamiento.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ ex);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                foreach (ClaseTratamiento tratamientos in dg_materiales.ItemsSource)
                {
                    if (tratamiento.NombreMaterial == dg_materiales.SelectedValue.ToString())
                        tratamiento.Cantidad = Convert.ToInt32(tratamientos.Cantidad.ToString());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                btnGuardar.IsEnabled = false;
                txtCantidad.IsEnabled = false;
                btnEditar.IsEnabled = true;
                btnRealizar.IsEnabled = true;
                cmbTratamiento.IsEnabled = true;
                btnCancelar.IsEnabled = false;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            txtCantidad.IsEnabled = false;
            btnEditar.IsEnabled = true;
            btnRealizar.IsEnabled = true;
            cmbTratamiento.IsEnabled = true;
            btnCancelar.IsEnabled = false;
        }
    }
}
