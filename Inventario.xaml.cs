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
        int num = 1;
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
    }
}

