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

// Agregar los namespaces necesarios para conectarse a SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Caja.xaml
    /// </summary>
    public partial class Caja : Window
    {

        private bool Admin;
        private String Nombree;
        private ClaseCaja caja = new ClaseCaja();
        
        public Caja()
        {
            InitializeComponent();
            MostrarCaja();
        }

        public Caja(bool admin, string name)
        {
            InitializeComponent();
            MostrarCaja();
            Nombree = name;
            Admin = admin;

        }

        public void MostrarCaja()
        {
            dgvCaja.ItemsSource = caja.MostrarCaja();
            dgvCaja.SelectedValuePath = "Id_transaccion";
            dgvCaja.SelectedIndex = dgvCaja.Items.Count-2;
        }

        private void ObtenerValues()
        {
            
            caja.Cantidad = Convert.ToInt32(txtCantidadCaja.Text);
            if (rbEgreso.IsChecked == true)
            {
                caja.Dinero_disponible = Convert.ToInt32(txtDineroCaja.Text)-caja.Cantidad;
                caja.Tipo_transacción = "Egreso";
            }
            else if(rbIngreso.IsChecked==true){
                caja.Dinero_disponible= Convert.ToInt32(txtDineroCaja.Text) + caja.Cantidad;
                caja.Tipo_transacción = "Ingreso";
            }
            caja.Fecha = DateTime.Now;
        }


        /// <summary>
        /// Verifica que todos los valores no esten vacios
        /// </summary>
        
            private bool VerificarValores()
            {
                if(txtCantidadCaja.Text == string.Empty || txtDineroCaja.Text == string.Empty)
                {
                    MessageBox.Show("Por favor ingrese todos los valores en las cajas de texto");
                    return false;
                } 
                else if(rbIngreso.IsChecked == false && rbEgreso.IsChecked == false)
                {
                    MessageBox.Show("Por favor escoga una opcion de transaccion.");
                    return false;
                }   

            return true;
            }



        public void LimpiarForm()
        {
            txtCantidadCaja.Text = string.Empty;
            txtDineroCaja.Text = string.Empty;
            rbEgreso.IsChecked = false;
            rbIngreso.Content = true;
        }
        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar los datos que fueron ingresados
            if (VerificarValores() == true)
            {

                try
                {
                    ObtenerValues();
                    caja.IngresarCaja(caja);

                    // Mensaje de insercion exitosa a la base
                    MessageBox.Show("Datos insertados correctamente");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha occurido un error al momento de insertar.");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MostrarCaja();
                }

            }
            else if(VerificarValores()==false)
            {
                MessageBox.Show("Ingrese una cantidad y seleccione una de las transacciones");
            }

        }
        private void SelectLast_Click(object sender, RoutedEventArgs e)
        {
            dgvCaja.SelectedIndex = dgvCaja.Items.Count - 1;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(Admin,Nombree);
            menu.Show();
            this.Hide();
        }

        private void dgvCaja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgvCaja.SelectedIndex = dgvCaja.Items.Count - 2;
        }
    }
}
