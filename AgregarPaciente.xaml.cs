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

// Agregar los namespaces necesarios para conectarse a SQL Server
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for AgregarPaciente.xaml
    /// </summary>
    public partial class AgregarPaciente : Window
    {
        Validaciones validaciones = new Validaciones();
        bool Admin;
        String Nombree;

        //Variable para definir el genero seleccionado por el radio boton
        ClasePaciente paciente = new ClasePaciente();

        //Constructores
        public AgregarPaciente()
        {
            
            InitializeComponent();

        }

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
            Pacientes pacientes = new Pacientes(Admin,Nombree);
            pacientes.Show();
            this.Hide();
        }

        private void ObtenerValores()
        {
            paciente.NombrePaciente = txtAgregarNombre.Text;
            paciente.ApellidoPaciente = txtAgregarApellido.Text;
            if (validaciones.VerificarFecha(Convert.ToDateTime(datePicker1.Text)))
                paciente.FechaNac = Convert.ToDateTime(datePicker1.Text);
            if(validaciones.VerificarNumero(txtAgregarTelefono.Text))
                paciente.Telefono = txtAgregarTelefono.Text;
            if(validaciones.VerificarIdentidad(txtAgregarIdentidad.Text))
                paciente.Id_paciente = txtAgregarIdentidad.Text;
            if (rbFemenino.IsChecked == true)
            {
                paciente.Genero = "Femenino";
            }else if(rbMasculino.IsChecked == true)
            {
                paciente.Genero = "Masculino";
            }
        }
        /// <summary>
        /// Ingresa los valores a la base de datos al presionar el botón
        /// </summary>
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {

            //Comprueba que todos los campos estén llenos
            if (txtAgregarApellido.Text == String.Empty || txtAgregarNombre.Text == String.Empty || txtAgregarIdentidad.Text == String.Empty || txtAgregarTelefono.Text == String.Empty || datePicker1.Text == String.Empty ||rbMasculino.IsChecked==false && rbFemenino.IsChecked==false)
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
                    paciente.AgregarPaciente(paciente);
                    MessageBox.Show("Datos ingresados correctamente.");
                    Limpiar();

                }
                //Lanza una excepcion si ocurre un fallo
                catch(Exception)
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

        private void txtAgregarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex reg = new Regex("[0-9]");
            bool b = reg.IsMatch(txtAgregarNombre.Text);
            if (b == true)
            {
                MessageBox.Show("Ingrese solamente caracteres");
                txtAgregarNombre.Text = "";
            }
        }

        private void txtAgregarApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex reg = new Regex("[0-9]");
            bool b = reg.IsMatch(txtAgregarApellido.Text);
            if (b == true)
            {
                MessageBox.Show("Ingrese solamente caracteres");
                txtAgregarApellido.Text = "";
            }
        }

        private void AddPaciente_Closed(object sender, EventArgs e)
        {
            Pacientes pacientes = new Pacientes(Admin, Nombree);
            pacientes.Show();
            this.Close();

        }
    }
}
