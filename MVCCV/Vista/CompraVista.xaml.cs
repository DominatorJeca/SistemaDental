using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        Validaciones val = new Validaciones();
        public Usuario user = new Usuario();
        ObservableCollection<ClaseInventario> productosCompra = new ObservableCollection<ClaseInventario>();
        private ICollectionView DatosDataGrid;
        ClaseProcedimiento proc = new ClaseProcedimiento();
        bool nuevoprod = false;

        public CompraVista()
        {
            InitializeComponent();
            fchVencimiento.BlackoutDates.AddDatesInPast();
            fchVencimiento.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, DateTime.Today));
        }
       
        private void btnComprar_Click(object sender, RoutedEventArgs e)
        {
            int compraId = proc.InsertarCompra(user.Ide);
            proc.InsertarLog(user.Ide, "Se realizó una commpra");
            foreach ( ClaseInventario inv in productosCompra )
            {
                proc.InsertarDetalleCompra(compraId, inv.IdMaterial, inv.Cantidad, inv.precio, inv.fechaVenc,inv.NombreMaterial,user);
                proc.InsertarLog(user.Ide, "Se ingresó un detalle de compra");

            }
            Grid_Loaded(null, null);
            if (nuevoprod)
            {
                nuevoprod = false;
                btnCancelar.IsEnabled = false;
                btnNuevoProducto.IsEnabled = true;
                txtNombreProd.IsEnabled = false;
            }
            productosCompra.Clear();
            btnQuitar.IsEnabled = false;
            btnComprar.IsEnabled = false;
            calculos();
        }
        
        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (val.VerificarCampos(this) && (dgv_Compras.SelectedItem!=null||nuevoprod))
            {
                ClaseInventario prod = new ClaseInventario();
                prod.IdMaterial = nuevoprod ? 0 : Convert.ToInt32(dgv_Compras.SelectedValue.ToString());  //Mopd
                prod.NombreMaterial = txtNombreProd.Text;
                prod.precio = float.Parse(txtprecio.Text);
                prod.fechaVenc = (DateTime)fchVencimiento.SelectedDate;
                prod.Cantidad = Convert.ToInt32(txtCantidad.Text);

                if (prod.Cantidad>1000 || prod.Cantidad <=0 )
                {
                    MessageBox.Show("Porfavor ingrese una cantidad valida");
                    return;
                }
              
                if (prod.precio>10000 || prod.precio <=0 )
                {
                    MessageBox.Show("Por favor ingrese un precio fijo");
                    return;
                }
                
                foreach(ClaseInventario inventario in DatosDataGrid)
                {
                    if (nuevoprod && inventario.NombreMaterial.ToLower().Contains(prod.NombreMaterial.ToLower()))
                    {
                        MessageBox.Show("Desea agregar un nuevo producto que ya existe en la base de datos. Proceda a seleccionarlo y agregarlo");
                        return;
                    }
                }

                foreach (ClaseInventario inv in productosCompra)

                    if ((inv.IdMaterial == prod.IdMaterial && !nuevoprod)||(nuevoprod && inv.NombreMaterial.ToLower()==prod.NombreMaterial.ToLower()))
                    {
                        MessageBox.Show("Este material ya existe eliminelo e ingreselo nuevamente");
                        return;
                    }
                productosCompra.Add(prod);
                dgv_Carrito.ItemsSource = productosCompra;
                dgv_Carrito.SelectedValuePath = "IdMaterial";
                calculos();
                dgv_Carrito.SelectedItem = -1;
                txtCantidad.Text = "";
                txtprecio.Text = "";
                fchVencimiento.SelectedDate = null;
                if (nuevoprod)
                {
                    nuevoprod = false;
                    btnCancelar.IsEnabled = false;
                    btnNuevoProducto.IsEnabled = true;
                    txtNombreProd.IsEnabled = false;
                    txtNombreProd.Text = "";
                }
                btnQuitar.IsEnabled = true;
                btnComprar.IsEnabled = true;

            }
            else
                MessageBox.Show("Por favor verifique todos los datos antes de ingresar un nuevo producto");
           
        }
      

        public void calculos ()
        {
            double totales=0;
            int cantidad=0;
            double subtotal=0;
            foreach (ClaseInventario prod in productosCompra)
            {
                cantidad += prod.Cantidad;
                subtotal += (prod.precio*prod.Cantidad);
            }
            totales = subtotal + ((subtotal) * 0.15);

            txtCantidadTotal.Text = Convert.ToString(cantidad);
            txtSubtotal.Text = Convert.ToString(subtotal);
            txtTotal.Text = Convert.ToString(totales);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DatosDataGrid = new CollectionViewSource() { Source = proc.MostrarInventario() }.View ;
            dgv_Compras.SelectedValuePath = "IdMaterial";
            dgv_Compras.ItemsSource = DatosDataGrid;

        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_Carrito.SelectedValue != null)
                productosCompra.RemoveAt(dgv_Carrito.SelectedIndex);
            else
                MessageBox.Show("Por favor seleccione un producto a eliminar");
            if(productosCompra.Count==0)
            {
                btnQuitar.IsEnabled = false;
                btnComprar.IsEnabled = false;
            }
            calculos();
        }

        private void dgv_Compras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_Compras.SelectedIndex != -1)
            {
                txtCantidad.Text = "";
                txtprecio.Text = "";
                fchVencimiento.SelectedDate = null;
                txtCantidad.IsEnabled = true;
                txtprecio.IsEnabled = true;
                fchVencimiento.IsEnabled = true;
            }
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {
            val.SoloLetras(e);
        }
        private void PreviewTextInputOnlyDecNumbers(object sender,TextCompositionEventArgs e)
        {
            val.SoloNumerosDec(e);
        }
        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            val.SoloNumeros(e);
        }

        private void btnNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            dgv_Compras.SelectedIndex = -1;
            dgv_Compras.IsEnabled = false;
            btnCancelar.IsEnabled = true;
            btnNuevoProducto.IsEnabled = false;
            nuevoprod = true;
            txtNombreProd.IsEnabled = true;
            txtCantidad.IsEnabled = true;
            txtprecio.IsEnabled = true;
            fchVencimiento.IsEnabled = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            nuevoprod = false;
            btnNuevoProducto.IsEnabled = true;
            dgv_Compras.SelectedIndex = 0;
            txtNombreProd.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            dgv_Compras.IsEnabled = true;
        }

        private void txtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscarProducto.Text == "")
            {
                DatosDataGrid.Filter = null;
                dgv_Compras.ItemsSource = DatosDataGrid;
            }
            else
            {
                var filtro = new Predicate<object>(item => (((ClaseInventario)item).NombreMaterial.ToLower()).Contains(txtBuscarProducto.Text.ToLower()));
                DatosDataGrid.Filter = filtro;
                dgv_Compras.ItemsSource = DatosDataGrid;
            }
        }
    }
}
