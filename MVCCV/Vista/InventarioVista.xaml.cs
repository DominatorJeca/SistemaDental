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

    }
}
