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
using System.Text.RegularExpressions;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Caja.xaml
    /// </summary>
    public partial class Caja : Window
    {
        private bool bandera;
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
            caja.Cantidad = (float)Convert.ToDecimal(txtCantidadCaja.Text);
                if (rbEgreso.IsChecked == true)
                {
 
                              caja.Dinero_disponible = (float)Convert.ToDecimal(txtDineroCaja.Text) - caja.Cantidad;
                              caja.Tipo_transacción = "Egreso";

                }
                else if (rbIngreso.IsChecked == true)
                {                
                    caja.Dinero_disponible = (float)Convert.ToDecimal(txtDineroCaja.Text) + caja.Cantidad;
                    caja.Tipo_transacción = "Ingreso";
                 }
                caja.Fecha = DateTime.Now;


           
            
           
            
           
        }


        /// <summary>
        /// Verifica que todos los valores no esten vacios
        /// </summary>
        
            private bool VerificarValores()
            {

            Regex reg = new Regex("[0-9]");
            bool b = reg.IsMatch(txtCantidadCaja.Text);

            if (txtCantidadCaja.Text == string.Empty || txtDineroCaja.Text == string.Empty)
             {
                    MessageBox.Show("Por favor ingrese la cantidad de dinero para realizar su transaccion");
                    return false;
             } 
            else if(rbIngreso.IsChecked == false && rbEgreso.IsChecked == false)
            {          
                    return false;
            }
            else if (b == false)
            {
                MessageBox.Show("No se aceptan letras en el campo de cantidad");
                return false;
            }
            else if ((float)Convert.ToDecimal(txtCantidadCaja.Text) >= (float)Convert.ToDecimal(txtDineroCaja.Text) && rbEgreso.IsChecked == true)
            {
                   MessageBox.Show("No hay suficiente fondo");
                  return false;
            }
            if ((float)Convert.ToDecimal(txtCantidadCaja.Text)>100000.00)
            {
                MessageBox.Show("No se permite hacer esta transacción, su limite es de 100,000.00 Lps");
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
                    MessageBox.Show("Se realizo con exito transaccion");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Oh No! algo Salio mal,revisa que la cantidad tenga el formato correcto y sea positivo");
                   
                }
                finally
                {
                    MostrarCaja();
                }

            }

            else if (rbIngreso.IsChecked==false && rbEgreso.IsChecked == false)
            {
                MessageBox.Show("Por favor selecione una opcion de transaccion");
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

        private void txtCantidadCaja_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void frmcaja_Closed(object sender, EventArgs e)
        {
            Menu menu = new Menu(Admin, Nombree);
            menu.Show();
            this.Close();
        }

      
    }
}
