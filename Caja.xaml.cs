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

        
        private ClaseCaja caja = new ClaseCaja();

        private SqlConnection sqlConnection;
        public Caja()
        {
            InitializeComponent();
            MostrarCaja();
            
        }

        public void MostrarCaja()
        {
            dgvCaja.ItemsSource = caja.MostrarCaja();
            dgvCaja.SelectedValuePath = "id_transaccion";
            dgvCaja.DisplayMemberPath = "tipo_transacción";
        }

        private void ObtenerValues()
        {
            
            caja.Cantidad = Convert.ToInt32(txtCantidadCaja.Text);
            caja.Dinero_disponible = Convert.ToInt32(txtDineroCaja.Text);

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
                else if(rbIngreso.IsChecked == false)
                {
                    MessageBox.Show("Por favor escoga una opcion de transaccion.");
                    return false;
                }   
                else if(rbEgreso.IsChecked == false)
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
            if(VerificarValores() == true)
            {

                try
                {
                    //Obtener los valores de caja
                    ObtenerValues();

                    //Insertar datos de Caja
                    caja.IngresarCaja(caja);

                    // Mensaje de insercion exitosa a la base
                    MessageBox.Show("Datos insertaos correctamente");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha occurido un error al momento de insertar el empleado");
                    Console.WriteLine(ex.Message);
                }
                
            }

        }
    }
}
