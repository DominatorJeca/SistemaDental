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
    /// Interaction logic for Citas.xaml
    /// </summary>
    public partial class Citas : Window
    {
        ClaseCitas citas = new ClaseCitas();
        int bandera = 0;
        private bool Admin;
        private String Nombree;
        public Citas()
        {
            InitializeComponent();
            MostrarDatos();
            mostrarCitas();
        }

        public Citas(bool admin, string name)
        {
            InitializeComponent();
            MostrarDatos();
            mostrarCitas();
            Nombree = name;
            Admin = admin;
        }

        private void MostrarDatos()
        {
            cmbPaciente.ItemsSource = citas.mostrarIdPacientes();
            cmbPaciente.DisplayMemberPath = "IdPacientes";
            cmbPaciente.SelectedValuePath = "IdPacientes";
            cmbEmpleado.ItemsSource = citas.MostrarEmpleado();
            cmbEmpleado.DisplayMemberPath = "NombreDoctor";
            cmbEmpleado.SelectedValuePath = "IdDoctor";
            cmbTratamiento.ItemsSource = citas.MostrarTratamiento();
            cmbTratamiento.DisplayMemberPath = "NombreTratamiento";
            cmbTratamiento.SelectedValuePath = "IdTratamiento";
        }

        private void mostrarCitas()
        {
            dtg_Citas.ItemsSource = citas.MostrarCitas();
            dtg_Citas.SelectedValuePath = "IdCita";
        }

        private void ObtenerValores()
        {
            citas.IdDoctor = cmbEmpleado.SelectedValue.ToString();
            citas.IdPacientes = cmbPaciente.SelectedValue.ToString();
            citas.IdTratamiento =Convert.ToInt32(cmbTratamiento.SelectedValue.ToString());
            citas.fechaCita = Convert.ToDateTime(SelectedDateTextBox.Text);
            
        }

        private void obtenerCita()
        {
            citas.IdCita = Convert.ToInt32(dtg_Citas.SelectedValue.ToString());
        }

        /// <summary>
        /// Con esta función del botón agregar se agendan las citas dentro del sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbEmpleado.SelectedValue.ToString() != null && cmbPaciente.SelectedValue.ToString() != null && Convert.ToInt32(cmbTratamiento.SelectedValue) > 0)
                {
                    ObtenerValores();
                    
                    //Se valida que no haya una cita registrada con detalles iguales
                    foreach(ClaseCitas citas in dtg_Citas.ItemsSource)
                    {
                        if (citas.IdPacientes == cmbPaciente.SelectedValue.ToString() && citas.fechaCita==Convert.ToDateTime(SelectedDateTextBox.Text) || citas.IdDoctor== cmbEmpleado.SelectedValue.ToString() && citas.fechaCita == Convert.ToDateTime(SelectedDateTextBox.Text))
                        {
                            bandera = 1;
                        }
                        else
                        {
                            bandera = 0;
                        }
                    }
                    if (bandera == 0)
                    {
                        citas.AgendarCita(citas);
                        MessageBox.Show("Cita agendada con éxito");
                    }
                    else
                    {
                        MessageBox.Show("Al parecer ya hay una cita agendada con ciertos parámetros iguales");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar todos los elementos necesarios para agendar una cita y debe revisar que su fecha sea la correcta");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Asegurese de ingresar una fecha correcta" + ex);
            }
            finally
            {
                cmbEmpleado.SelectedValue = null;
                cmbPaciente.SelectedValue = null;
                cmbTratamiento.SelectedValue = 0;
                mostrarCitas();
            }
        }

        /// <summary>
        /// Con esta función del botón editar se bloquean o desbloquean objetos para editar una cita
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (dtg_Citas.SelectedValue !=null)
                {
                    btnGuardar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnAgregar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    btnBorrar.IsEnabled = false;
                    dtg_Citas.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Seleccione una cita agendada");
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        /// <summary>
        /// Con el botón guardar se registra la edición de una cita al sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                obtenerCita();
                ObtenerValores();
                //Se valida que no haya una cita registrada con detalles iguales
                foreach (ClaseCitas citas in dtg_Citas.ItemsSource)
                {
                    if (citas.IdPacientes == cmbPaciente.SelectedValue.ToString() && citas.fechaCita == Convert.ToDateTime(SelectedDateTextBox.Text) || citas.IdDoctor == cmbEmpleado.SelectedValue.ToString() && citas.fechaCita == Convert.ToDateTime(SelectedDateTextBox.Text))
                    {
                        bandera = 1;
                    }
                    else
                    {
                        bandera = 0;
                    }
                }
                if (bandera == 0)
                {
                    citas.EditarCita(citas);
                    MessageBox.Show("Exito al editar");
                }
                else
                {
                    MessageBox.Show("Al parecer ya hay una cita agendada con ciertos parámetros iguales");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                mostrarCitas();
                btnGuardar.IsEnabled = false;
                btnEditar.IsEnabled = true;
                btnAgregar.IsEnabled = true;
                btnCancelar.IsEnabled = false;
                btnBorrar.IsEnabled = true;
                dtg_Citas.IsEnabled = true;
            }
        }

        /// <summary>
        /// COn esta función el textbox para la fecha obtiene su valor basado en la selección del calendario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdCitas_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDateTextBox.Text = cdCitas.SelectedDate.ToString();
        }

        private void dtg_Citas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// COn el botón borrar se elimina del registro la cita agendada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtg_Citas.SelectedValue != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("¿Seguro que desea eliminar la cita?", "Eliminar Cita", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        obtenerCita();
                        citas.EliminarCita();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una cita para poder eliminarla");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                mostrarCitas();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dtg_Citas.IsEnabled = true;
            mostrarCitas();
            btnAgregar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            btnBorrar.IsEnabled = true;
            btnCancelar.IsEnabled = false;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }
    }
}
