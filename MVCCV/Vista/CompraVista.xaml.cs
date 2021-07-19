using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CompraVista.xaml
    /// </summary>
    public partial class CompraVista : UserControl
    {
        public CompraVista()
        {
            InitializeComponent();
        }
        ObservableCollection<ClaseInventario> productosCompra = new ObservableCollection<ClaseInventario>();
        private ObservableCollection<ClaseInventario> DatosDataGrid = new ObservableCollection<ClaseInventario>();
        ClaseProcedimiento proc = new ClaseProcedimiento();
        private void btnComprar_Click(object sender, RoutedEventArgs e)
        {
            int compraId = proc.InsertarCompra(1);

            foreach ( ClaseInventario inv in productosCompra )
            {
                proc.InsertarDetalleCompra(compraId, inv.IdMaterial, inv.Cantidad, inv.precio, inv.fechaVenc);
            }
        }
        
        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            ClaseInventario prod = new ClaseInventario();
            prod.IdMaterial = Convert.ToInt32(dgv_Compras.SelectedValue.ToString());
            prod.NombreMaterial = txtNombreProd.Text;
            prod.precio = Convert.ToInt32(txtprecio.Text);
            prod.fechaVenc =(DateTime)fchVencimiento.SelectedDate;
            prod.Cantidad = Convert.ToInt32(txtCantidad.Text);
            foreach (ClaseInventario inv in productosCompra)
                if (inv.IdMaterial==prod.IdMaterial)
                {
                    MessageBox.Show("Este material ya existe eliminelo e ingreselo nuevamente");
                        return;
                }    
            productosCompra.Add(prod);
            dgv_Carrito.ItemsSource = productosCompra;
            dgv_Carrito.SelectedValuePath = "IdMaterial";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DatosDataGrid = new ObservableCollection<ClaseInventario>(proc.MostrarInventario());
            dgv_Compras.ItemsSource = DatosDataGrid;
            dgv_Compras.SelectedValuePath = "IdMaterial";

        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if(dgv_Carrito.SelectedValue!=null)
        
                 productosCompra.RemoveAt(dgv_Carrito.SelectedIndex);
           
        }

        private void dgv_Compras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCantidad.Text = "";
            txtprecio.Text = "";
            fchVencimiento.SelectedDate = null; 
        }
    }
}
