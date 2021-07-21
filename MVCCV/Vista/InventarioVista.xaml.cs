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
        private bool Admin;
        private String Nombree;

        public InventarioVista()
        {
            InitializeComponent();
            MostrarMaterial();

        }

        public InventarioVista(bool admin, string name)
        {
            InitializeComponent();
            MostrarMaterial();
            Nombree = name;
            Admin = admin;

        }

        public void MostrarMaterial()
        {
           // dgv_Materiales.ItemsSource = inventario.MostrarInventario();
            dgv_Materiales.SelectedValuePath = "IdMaterial";
        }




        private void dgv_Materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            dgv_Tratamiento.ItemsSource = inventario.mostrarUsoTratamiento(Convert.ToInt32(dgv_Materiales.SelectedValue)).DefaultView;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_Materiales.SelectedValue != null)
            {
                txtCantidadInventario.IsEnabled = true;
                btnGuardar.IsEnabled = true;
                btnEditar.IsEnabled = false;
                OcultarMostrarBotones(Visibility.Visible);
            }
            else
            {
                MessageBox.Show("Seleccione un material");
            }
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
                    if (Convert.ToInt32(txtCantidadInventario.Text) >= 1)
                    {
                        ObtenerValores();
                        inventario.actualizarCantidad(inventario);
                    }
                    else
                    {
                        MostrarMaterial();
                        MessageBox.Show("No puede ingresar una cantidad menor que 1");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Asegurese de ingresar una cantidad mayor a 0...");

                }
                finally
                {
                    MostrarMaterial();
                    btnEditar.IsEnabled = true;
                    btnGuardar.IsEnabled = false;
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

        /*
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            string msg = "Close or not?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                // If user doesn't want to close, cancel closure
                Inventario inventario = new Inventario();
                inventario.Show();
            }
            else
            {
                Menu menu = new Menu();
                menu.Show();
            }
        }*/
    }
}
