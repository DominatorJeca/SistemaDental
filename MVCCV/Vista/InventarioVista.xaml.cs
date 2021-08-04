using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para InventarioVista.xaml
    /// </summary>
    public partial class InventarioVista : UserControl
    {
        private ClaseInventario inventario = new ClaseInventario();
        private ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        private Validaciones validaciones = new Validaciones();
        private ObservableCollection<ClaseInventario> DatosDataGrid = new ObservableCollection<ClaseInventario>();
        public InventarioVista()
        {
            InitializeComponent();
        }



        private void dgv_Materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            dgv_Tratamiento.ItemsSource = procedimiento.mostrarUsoTratamiento(Convert.ToInt32(dgv_Materiales.SelectedValue));
        }

        private void HabilitarEdicion(bool habilitar)
        {
            txtMaterialInventario.IsEnabled = habilitar;
            btnCancelar.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
        }

        private void ObtenerValores()
        {
            inventario.IdMaterial = Convert.ToInt32(dgv_Materiales.SelectedValue);
            inventario.NombreMaterial = txtMaterialInventario.Text;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (validaciones.VerificarCampos(this) && dgv_Materiales.SelectedValue != null)
            {
                HabilitarEdicion(true);
                btnActualizar.IsEnabled = false;
                txtMaterialInventario.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Seleccione un material");
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerValores();
            if (validaciones.VerificarCampos(this))
            {
                procedimiento.EditarNombreMaterial(inventario);
                Grid_Loaded_1(null, null);
                HabilitarEdicion(false);
                btnActualizar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar el nombre del material de manera correcta");
                Grid_Loaded_1(null, null);
                HabilitarEdicion(false);
                btnActualizar.IsEnabled = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Grid_Loaded_1(null, null);
            HabilitarEdicion(false);
            btnActualizar.IsEnabled = true;
        }

        private void PreviewTextInputLetters(object sender, TextCompositionEventArgs e)
        {
            validaciones.SoloLetras(e);
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            DatosDataGrid = new ObservableCollection<ClaseInventario>(procedimiento.MostrarInventario());
            dgv_Materiales.ItemsSource = DatosDataGrid;
            dgv_Materiales.SelectedValuePath = "IdMaterial";
        }
    }
}
