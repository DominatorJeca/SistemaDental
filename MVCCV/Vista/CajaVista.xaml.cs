using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para CajaVista.xaml
    /// </summary>
    public partial class CajaVista : UserControl
    {
        private bool bandera;
        private bool Admin;
        private String Nombree;
        private ClaseCaja caja = new ClaseCaja();

        public CajaVista()
        {
            InitializeComponent();
            MostrarCaja();
        }

        public CajaVista(bool admin, string name)
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
            dgvCaja.SelectedIndex = dgvCaja.Items.Count - 2;
        }

        private void ObtenerValues()
        {
            caja.Cantidad = (float)Convert.ToDecimal(txtCantidadCaja.Text);









        }


        /// <summary>
        /// Verifica que todos los valores no esten vacios
        /// </summary>

        


      
        private void SelectLast_Click(object sender, RoutedEventArgs e)
        {
            dgvCaja.SelectedIndex = dgvCaja.Items.Count - 1;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            
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
          
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
