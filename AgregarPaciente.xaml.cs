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
        
        //Variables Miembro
        private SqlConnection sqlConnection;

        //Constructores

        public AgregarPaciente()
        {
            
            //Creacion de Conexion con la Base de Datos
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaDental.Properties.Settings.ClinicaBDConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes();
            pacientes.Show();
        }


        /// <summary>
        /// Ingresa los valores a la base de datos al presionar el botón
        /// </summary>
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {

            //Comprueba que todos los campos estén llenos
            if (txtAgregarApellido.Text == String.Empty || txtAgregarNombre.Text == String.Empty || txtAgregarIdentidad.Text == String.Empty || txtAgregarTelefono.Text == String.Empty || cmbSexo.SelectedItem == null)
            {
                MessageBox.Show("¡Por favor llena todos los campos!");
                txtAgregarNombre.Focus();
            }
            //Intenta ingresar datos a la BD
            else
            {
                try
                {

                }
                //Lanza una excepcion si ocurre un fallo
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                // Cierra la Conexión a la Base de Datos
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
    }
}
