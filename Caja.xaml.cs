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


        ClaseCaja caja = new ClaseCaja();

        private SqlConnection sqlConnection;
        public Caja()
        {
            InitializeComponent();
            MostrarCaja();
            IngresarDatos();
        }

        public void MostrarCaja()
        {
            dgvCaja.ItemsSource = caja.MostrarCaja();
            dgvCaja.SelectedValuePath = "id_transaccion";
        }

        public void IngresarDatos()
        {
            dgvCaja.ItemsSource = caja.IngresarDatos();
            dgvCaja.SelectedValuePath = "id_transaccion";
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(dgvCaja.SelectedValue) > 0)
            {
                try
                {
                    ObtenerValues();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void ObtenerValues()
        {
            caja.cantidad = Convert.ToInt32(txtCantidadCaja.Text);
            caja.dinero_disponible = Convert.ToInt32(txtDineroCaja.Text);
            
        }
    }
}
