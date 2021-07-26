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
        private bool Admin;
        private String Nombree;

        public Inventario()
        {
            InitializeComponent();
            MostrarMaterial();

        }

        public Inventario(bool admin, string name)
        {
            
        }

        public void MostrarMaterial()
        {
           
        }

       

   
        private void dgv_Materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
   
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ObtenerValores()
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void OcultarMostrarBotones(Visibility ocultar)
        {
            
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
          
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
          
        }
    }
}

