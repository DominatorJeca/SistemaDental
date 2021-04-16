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

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for AgregarPaciente.xaml
    /// </summary>
    public partial class AgregarPaciente : Window
    {
        
        //Variable para la conexion con la base de datos
        private SqlConnection sqlConnection;

        //Variable para definir el genero seleccionado por el radio boton
        ClasePaciente paciente = new ClasePaciente();

        //Constructores
        public AgregarPaciente()
        {
            
            InitializeComponent();

            //Creacion de Conexion con la Base de Datos
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

        }

        /// <summary>
        /// Boton para regresar a la ventana anterior
        /// </summary>
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            pacientes.Show();
            this.Close();
        }

        private void ObtenerValores()
        {
            paciente.NombrePaciente = txtAgregarNombre.Text;
            paciente.ApellidoPaciente = txtAgregarApellido.Text;
            paciente.Edad = Convert.ToInt32(txtAgregarEdad.Text);
            paciente.Telefono = txtAgregarTelefono.Text;
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
            if (txtAgregarApellido.Text == String.Empty || txtAgregarNombre.Text == String.Empty || txtAgregarIdentidad.Text == String.Empty || txtAgregarTelefono.Text == String.Empty || txtAgregarEdad.Text == String.Empty ||rbMasculino.IsChecked==false && rbFemenino.IsChecked==false)
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
                // Cierra la Conexión a la Base de Datos
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        private void Limpiar()
        {
            txtAgregarApellido.Text = "";
            txtAgregarNombre.Text = "";
            txtAgregarEdad.Text = "";
            txtAgregarIdentidad.Text = "";
            txtAgregarTelefono.Text = "";
            rbFemenino.IsChecked = false;
            rbMasculino.IsChecked = false;
        }
    }
}
