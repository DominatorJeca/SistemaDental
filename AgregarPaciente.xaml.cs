using System;
using System.Windows;
using System.Windows.Input;

// Agregar los namespaces necesarios para conectarse a SQL Server

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for AgregarPaciente.xaml
    /// </summary>
    public partial class AgregarPaciente : Window
    {
        private Validaciones validaciones = new Validaciones();
        private bool Admin;
        private String Nombree;

        //Variable para definir el genero seleccionado por el radio boton
        private ClasePaciente paciente = new ClasePaciente();

        //Constructores
        public AgregarPaciente()
        {

            InitializeComponent();

        }

        private Validaciones validar = new Validaciones();
        public AgregarPaciente(bool admin, string name)
        {

            InitializeComponent();
            Nombree = name;
            Admin = admin;

        }

        /// <summary>
        /// Boton para regresar a la ventana anterior
        /// </summary>
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ObtenerValores()
        {

        }
        /// <summary>
        /// Ingresa los valores a la base de datos al presionar el botón
        /// </summary>
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {

            //Comprueba que todos los campos estén llenos
            if (txtAgregarApellido.Text == String.Empty || txtAgregarNombre.Text == String.Empty || txtAgregarIdentidad.Text == String.Empty || txtAgregarTelefono.Text == String.Empty || datePicker1.Text == String.Empty || rbMasculino.IsChecked == false && rbFemenino.IsChecked == false)
            {
                MessageBox.Show("¡Por favor llena todos los campos!");
                txtAgregarNombre.Focus();
            }
            //Intenta ingresar datos a la BD
            else
            {
                try
                {
                    ObtenerValores();
                    // paciente.AgregarPaciente(paciente);
                    MessageBox.Show("Datos ingresados correctamente.");
                    Limpiar();

                }
                //Lanza una excepcion si ocurre un fallo
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error, revise sus datos e intente de nuevo");
                }

            }

        }

        private void Limpiar()
        {
            txtAgregarApellido.Text = "";
            txtAgregarNombre.Text = "";
            datePicker1.Text = "";
            txtAgregarIdentidad.Text = "";
            txtAgregarTelefono.Text = "";
            rbFemenino.IsChecked = false;
            rbMasculino.IsChecked = false;
        }



        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {


            validar.SoloLetras(e);

        }


        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {


            validar.SoloNumeros(e);
        }

        private void AddPaciente_Closed(object sender, EventArgs e)
        {


        }
    }
}
