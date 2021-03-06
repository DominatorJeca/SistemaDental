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

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for VerPaciente.xaml
    /// </summary>
    public partial class VerPaciente : Window
    {

        //Creacion de un objeto de tipo ClasePaciente
        private ClasePaciente unPaciente = new ClasePaciente();

        private bool Admin;
        private String Nombree;

        /// <summary>
        /// Componentes que se inicializan junto al form
        /// </summary>
        public VerPaciente()
        {
            InitializeComponent();
            MostrarPacientes();
            HabilitacionDeshabilitacion(false, true);
        }

        public VerPaciente(bool admin, string name)
        {
            InitializeComponent();
            MostrarPacientes();
            HabilitacionDeshabilitacion(false, true);
            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Boton para regresar al menu anterior
        /// </summary>
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Pacientes pacientes = new Pacientes(Admin,Nombree);
            pacientes.Show();
            this.Hide();
        }

        /// <summary>
        /// Funcion para llenar el combobox con los id extraidos de la BD mediante la clase ClasePaciente
        /// </summary>
        public void MostrarPacientes()
        {
            cmbPaciente.ItemsSource = unPaciente.MostrarPacientes();
            cmbPaciente.SelectedValuePath = "Id_paciente";
            cmbPaciente.DisplayMemberPath = "Id_paciente";
        }

   

        /// <summary>
        /// Funcion para habilitar o deshabilitar los botones, textbox y combobox del formulario
        /// </summary>
        public void HabilitacionDeshabilitacion(bool habilitacionGrupoA, bool habilitacionGrupoB)
        {
            btnGuardarPaciente.IsEnabled = habilitacionGrupoA;
            btnCancelar.IsEnabled = habilitacionGrupoA;
            txtNombre.IsEnabled = habilitacionGrupoA;
            txtApellido.IsEnabled = habilitacionGrupoA;
            txtTelefono.IsEnabled = habilitacionGrupoA;
            txtIdentidad.IsEnabled = habilitacionGrupoA;
            txtEdad.IsEnabled = habilitacionGrupoA;
            cmbGenero.IsEnabled = habilitacionGrupoA;
            btnEditarPaciente.IsEnabled = habilitacionGrupoB;
            cmbPaciente.IsEnabled = habilitacionGrupoB;
        }

        /// <summary>
        /// Funcion para limpiar el contenido del Form
        /// </summary>
        public void LimpiarPantalla ()
        {
            txtNombre.Text = null;
            txtApellido.Text = null;
            txtIdentidad.Text = null;
            txtTelefono.Text = null;
            txtEdad.Text = null;
            dtgHistorial.ItemsSource = null;
            cmbPaciente.SelectedValue = null;
        }

        public void obtenerValores()
        {
            unPaciente.NombrePaciente = txtNombre.Text;
            unPaciente.ApellidoPaciente = txtApellido.Text;
            unPaciente.Edad = Convert.ToInt32(txtEdad.Text);
            unPaciente.Genero = cmbGenero.Text;
            unPaciente.Telefono = txtTelefono.Text;
            unPaciente.Id_paciente = txtIdentidad.Text;
            
        }

        /// <summary>
        /// Boton para editar el paciente seleccionado en el combobox de Id Paciente
        /// </summary>
        private void btnEditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Mensaje de advertencia si no selecciona ningun elemento
                if (cmbPaciente.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un paciente.");
                }
                else {
                    //Deshabilita el combobox paciente, el boton editar y ver
                    //Habilita los demás botones y textbox para poder editar los datos
                    HabilitacionDeshabilitacion(true, false);
                    

                }
            
            }
            catch
            {MessageBox.Show("Ha ocurrido un error en el sistema.");}
        }

        /// <summary>
        /// Boton para cancelar los cambios hechos y para limpiar la pantalla
        /// </summary>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //llama la funcion para deshabilitar los textbox y botones
                HabilitacionDeshabilitacion(false, true);
                //llama la funcion para limpiar pantalla
                LimpiarPantalla();
                MessageBox.Show("Se han cancelado los cambios.");
            }

            catch { MessageBox.Show("Ha ocurrido un error en el sistema."); }
        }

        /// <summary>
        /// Boton Ver el historial del paciente seleccionado en el combobox
        /// </summary>
       

        private void btnGuardarPaciente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    obtenerValores();
                    unPaciente.ActualizarDatosPaciente(unPaciente);
                    MessageBox.Show("Éxito al actualizar los datos");
                    LimpiarPantalla();
       
            }
            catch
            {
                throw;
            }
            finally
            {
                MostrarPacientes();
                HabilitacionDeshabilitacion(false, true);
            }
          


        }

        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPaciente.SelectedValue != null)
            {
                unPaciente.Id_paciente = cmbPaciente.SelectedValue.ToString();
                dtgHistorial.ItemsSource = unPaciente.MostrarHistorial(unPaciente);
            }
            
        }

        private void btnGuardarPaciente_Click()
        {

        }
    }
}
