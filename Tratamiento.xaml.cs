using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Tratamiento.xaml
    /// </summary>
    public partial class Tratamiento : Window
    {
        
        ClaseTratamiento tratamiento = new ClaseTratamiento();
        DateTime DateTime = new DateTime();
        private bool Admin;
        private String Nombree;
        int cant_anterior = 0;
        Validaciones validar = new Validaciones();
        public Tratamiento()
        {
            InitializeComponent();
            MostrarTratamientos();
            MostrarPacientes();
        }
        public Tratamiento(bool admin, string name)
        {
            InitializeComponent();
            MostrarTratamientos();
            MostrarPacientes();
            Nombree = name;
            Admin = admin;
        }

        public void MostrarTratamientos()
        {

            cmbTratamiento.DisplayMemberPath = "NombreTratamiento";
            cmbTratamiento.SelectedValuePath = "IdTratamiento";
        }

        public void MostrarPacientes()
        {

            cmbPaciente.DisplayMemberPath = "IdPaciente";
            cmbPaciente.SelectedValuePath = "IdPaciente";
        }

        public void MostrarMateriales()
        {

            dg_materiales.SelectedValuePath = "NombreMaterial";
        }

        private void cmbTratamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarMateriales();
        }

        public void ObtenerValores()
        {
  
            tratamiento.IdTratamiento = Convert.ToInt32(cmbTratamiento.SelectedValue);
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            
           
            
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            
           /* try
            {
                if (dg_materiales.SelectedValue != null || validar.VerificarCampos(this) )
                {

                    txtCantidad.IsEnabled = true;
                    btnGuardar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnRealizar.IsEnabled = false;
                    cmbTratamiento.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Seleccione un tratamiento y un material para poder ser editado");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ ex);
            }*/
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            txtCantidad.IsEnabled = false;
            btnEditar.IsEnabled = true;
            btnRealizar.IsEnabled = true;
            cmbTratamiento.IsEnabled = true;
            btnCancelar.IsEnabled = false;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            /*Menu menu = new Menu(Admin,Nombree);
            menu.Show();
            this.Hide();*/
        }

        private void dg_materiales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        { 

            validar.SoloNumeros(e);
        }

        private void TratamientoWindow_Closed(object sender, EventArgs e)
                {
                    Menu menu = new Menu();
                    menu.Show();
                }

        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
