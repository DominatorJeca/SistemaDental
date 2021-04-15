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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using MessageBox = System.Windows.MessageBox;
using DataGridCell = System.Windows.Forms.DataGridCell;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Inventario.xaml
    /// </summary>
    public partial class Inventario : Window
    {
        ClaseInventario inventario = new ClaseInventario();
       
        public Inventario()
        {
            InitializeComponent();
            MostrarMaterial();

        }

        public void MostrarMaterial()
        {
            dgv_Materiales.ItemsSource = inventario.MostrarInventario();
            dgv_Materiales.SelectedValuePath = "IdMaterial";
        }

       

   
        private void dgv_Materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
   
            dgv_Tratamiento.ItemsSource = inventario.mostrarUsoTratamiento(Convert.ToInt32(dgv_Materiales.SelectedValue)).DefaultView;
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtCantidadInventario.IsEnabled = true;
            txtMaterialInventario.IsEnabled= true;
            btnGuardar.IsEnabled = true;
            btnEditar.IsEnabled = false;
            OcultarMostrarBotones(Visibility.Visible);
        }

        private void ObtenerValores()
        {
            inventario.NombreMaterial = txtMaterialInventario.Text;
            inventario.Cantidad = Convert.ToInt32(txtCantidadInventario.Text);
            inventario.IdMaterial = Convert.ToInt32(dgv_Materiales.SelectedValue);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if (Convert.ToInt32(dgv_Materiales.SelectedValue) > 0)
            {

                try
                {
                    ObtenerValores();
                    inventario.actualizarCantidad(inventario);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Asegurese de ingresar una cantidad mayor a 0..."+ex);
                }
                finally
                {
                    MostrarMaterial();
                    btnEditar.IsEnabled = true;
                    txtCantidadInventario.IsEnabled = false;
                    OcultarMostrarBotones(Visibility.Hidden);
                }
            }
            else
            {
                MessageBox.Show("Para editar ocupa seleccionar una casilla");
            }
        }

        private void OcultarMostrarBotones(Visibility ocultar)
        {
            btnCancelarCambios.Visibility = ocultar;
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            txtCantidadInventario.IsEnabled = false;
            txtMaterialInventario.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnEditar.IsEnabled = true;
            OcultarMostrarBotones(Visibility.Hidden);
            MostrarMaterial();
        }
    }
}

