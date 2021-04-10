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

        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
