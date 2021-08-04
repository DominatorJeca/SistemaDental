using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Inventario.xaml
    /// </summary>
    public partial class Inventario : Window
    {
        private ClaseInventario inventario = new ClaseInventario();

        private bool Admin;
        private String Nombree;
        private Validaciones validar = new Validaciones();
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

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            validar.SoloNumeros(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
