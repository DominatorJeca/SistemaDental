﻿using System;
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
        public Citas()
        {
            InitializeComponent();
            MostrarDatos();
            mostrarCitas();
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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbEmpleado.SelectedValue.ToString() != null && cmbPaciente.SelectedValue.ToString() != null && Convert.ToInt32(cmbTratamiento.SelectedValue) > 0)
                {
                    ObtenerValores();
                    citas.AgendarCita(citas);
                    MessageBox.Show("Cita agendada con éxito");
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                obtenerCita();
                ObtenerValores();
                citas.EditarCita(citas);
                MessageBox.Show("Exito al editar");
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

        private void cdCitas_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDateTextBox.Text = cdCitas.SelectedDate.ToString();
        }

        private void dtg_Citas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

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
    }
}
