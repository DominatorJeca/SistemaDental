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
        private string genero;

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


        /// <summary>
        /// Ingresa los valores a la base de datos al presionar el botón
        /// </summary>
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {

            //Comprueba que todos los campos estén llenos
            if (txtAgregarApellido.Text == String.Empty || txtAgregarNombre.Text == String.Empty || txtAgregarIdentidad.Text == String.Empty || txtAgregarTelefono.Text == String.Empty || txtAgregarEdad.Text == String.Empty)
            {
                MessageBox.Show("¡Por favor llena todos los campos!");
                txtAgregarNombre.Focus();
            }
            //Intenta ingresar datos a la BD
            else
            {
                try
                {

                    // Query de inserción con procedimientos almacenados
                    string query = @"EXECUTE IngresoPacientes @id,@nombre,@apellido,@telefono,@edad,@genero,@estado";
                    // Establecer la conexión con la base de datos
                    sqlConnection.Open();

                    // Definir un SqlCommand para modificar el valor del parámetro
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    // Reemplazar el parámetro por su valor actual
                    sqlCommand.Parameters.AddWithValue("@nombre", txtAgregarNombre.Text);
                    sqlCommand.Parameters.AddWithValue("@apellido", txtAgregarApellido.Text);
                    sqlCommand.Parameters.AddWithValue("@telefono", txtAgregarTelefono.Text);
                    sqlCommand.Parameters.AddWithValue("@id", txtAgregarIdentidad.Text);
                    sqlCommand.Parameters.AddWithValue("@edad", txtAgregarEdad.Text);
                    sqlCommand.Parameters.AddWithValue("@genero", genero);
                    sqlCommand.Parameters.AddWithValue("@estado", 1);

                    // Ejecuta el NonQuery para utilizar el procedimiento almacenado escrito arriba
                    sqlCommand.ExecuteNonQuery();

                    // Limpa todos los textbox del formulario
                    txtAgregarNombre.Text = String.Empty;
                    txtAgregarApellido.Text = String.Empty;
                    txtAgregarTelefono.Text = String.Empty;
                    txtAgregarIdentidad.Text = String.Empty;
                    txtAgregarEdad.Text = String.Empty;
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

        //Asignación de la variable genero dependiendo del Radio Boton Seleccionado

        /// <summary>
        /// Asigna Masculino si el Radio Boton Masculino está seleccionado
        /// </summary>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            genero = "Masculino";
        }

        /// <summary>
        /// Asigna Femenino si el Radio Boton Femenino está seleccionado
        /// </summary>
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            genero = "Femenino";
        }
    }
}
