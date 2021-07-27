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
    /// Lógica de interacción para InventarioVista.xaml
    /// </summary>
    public partial class InventarioVista : UserControl
    {
        ClaseInventario inventario = new ClaseInventario();
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        
        
        public InventarioVista()
        {
            InitializeComponent();
            MostrarMaterial();

        }

        public void MostrarMaterial()
        {
            dgv_Materiales.ItemsSource = procedimiento.MostrarInventario();
            dgv_Materiales.SelectedValuePath = "IdMaterial";
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
            HabilitarEdicion(true);
            btnActualizar.IsEnabled = false;
            txtMaterialInventario.IsEnabled = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerValores();
            procedimiento.EditarNombreMaterial(inventario);
            MostrarMaterial();
            HabilitarEdicion(false);
            btnActualizar.IsEnabled = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MostrarMaterial();
            HabilitarEdicion(false);
            btnActualizar.IsEnabled = true;
        }
    }
}
