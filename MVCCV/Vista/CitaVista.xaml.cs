using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para CitaVista.xaml
    /// </summary>
    public partial class CitaVista : UserControl
    {
        ObservableCollection<ClaseCitas> tratamientos = new ObservableCollection<ClaseCitas>();
        ClaseCitas citas = new ClaseCitas();
        public event EventHandler CambioDeVistaPrincipal;
      
        int bton = 0;
        private bool Admin;
        private String Nombree;
        public CitaVista()
        {
            InitializeComponent();
            MostrarDatos();
            mostrarCitas();

        }

        public CitaVista(bool admin, string name)
        {
            InitializeComponent();
            MostrarDatos();
            mostrarCitas();
            Nombree = name;
            Admin = admin;
        }
        protected virtual void CambioDeVista(object o)
        {
            if (MenuNavegacion.IsTopDrawerOpen)
                MenuNavegacion.IsTopDrawerOpen = false;
            if (CambioDeVistaPrincipal != null)
                CambioDeVistaPrincipal(o, null);
        }
        private void Agregar_Tratamientos_Click(object sender, RoutedEventArgs e)
        {
            ClaseCitas prod = new ClaseCitas();
            prod.IdTratamiento = Convert.ToInt32(cmbTratamiento.SelectedValue.ToString());
            citas.mostraridtrtamientos(citas, prod.IdTratamiento);
            prod.nombreTramientoindividual = citas.nombreTramientoindividual;
            prod.trtamientoprecio = citas.trtamientoprecio;
            foreach (ClaseCitas inv in tratamientos)
                if (inv.IdTratamiento == prod.IdTratamiento)
                {
                    MessageBox.Show("Este material ya existe eliminelo e ingreselo nuevamente");
                    return;
                }
            tratamientos.Add(prod);
            dtg_Tratamientos.ItemsSource = tratamientos;
            dtg_Tratamientos.SelectedValuePath = "IdTratamiento";
        }
        private void MostrarDatos()
        {
            cmbPaciente.ItemsSource = citas.mostrarPacientes();


            cmbPaciente.DisplayMemberPath = "Nombre_Id_paciente";
            cmbPaciente.SelectedValuePath = "IdPacientes";
            cmbEmpleado.ItemsSource = citas.MostrarEmpleado();
            cmbEmpleado.DisplayMemberPath = "NombreDoctor";
            cmbEmpleado.SelectedValuePath = "IdEmpleado";
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


            citas.IdEmpleado = cmbEmpleado.SelectedValue.ToString();
            citas.IdPacientes = cmbPaciente.SelectedValue.ToString();
            citas.IdTratamiento = Convert.ToInt32(cmbTratamiento.SelectedValue.ToString());
            DateTime cmb = cdCitas.SelectedDate.Value.Add(ctTiempo.SelectedTime.Value.TimeOfDay);
            citas.fechaCita = cmb;
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


            btnGuardar.IsEnabled = true;
            btnEditar.IsEnabled = false;
            btnAgregar.IsEnabled = false;
            btnCancelar.IsEnabled = true;
            btnelimtratamiento.IsEnabled = true;
            btnelimtratamiento_cita_Copy.IsEnabled = false;
            dtg_Citas.IsEnabled = false;
            bton = 0;
            encenderbotones();
            limpiar();

        }

        private bool RevisarDatos()
        {
            if (cmbEmpleado.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione el doctor");
                return false;
            }
            if (cmbPaciente.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione el paciente");
                return false;
            }
            if (cmbTratamiento.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione el tratamiento");
                return false;
            }
            if (cdCitas.SelectedDate == null)
            {
                MessageBox.Show("Selecione una fecha");
                return false;
            }
            if (RevisarFecha())
            {
                MessageBox.Show("Por favor, selecione una fecha validad");
                return false;
            }
            if (ctTiempo.SelectedTime == null)
            {
                MessageBox.Show("Por favor, selecione una hora");
                return false;
            }
            if(tratamientos.Count<1)
            {
                MessageBox.Show("Por favor, Ingrese tratamientos a la lista");
                return false;
            }

            return true;
        }

        private bool RevisarFecha()
        {
            if (cdCitas.SelectedDate.Value.Date == DateTime.Now.Date)
            {
                return true;
            }
            if (cdCitas.SelectedDate.Value.Date < DateTime.Now.Date)
            {
                return true;
            }

            return false;
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

                if (dtg_Citas.SelectedValue != null)
                {
                    btnGuardar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnAgregar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    btnelimtratamiento.IsEnabled = true;
                    dtg_Citas.IsEnabled = false;
                    btnelimtratamiento_cita_Copy.IsEnabled = false;
                    bton = 1;
                    encenderbotones();

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
                if (bton == 1)
                  {
                    if (RevisarDatos() is true)
                     {


                        obtenerCita();
                        ObtenerValores();
                        //Se valida que no haya una cita registrada con detalles iguales


                        citas.EditarCita(citas);
                        citas.eliminardetallecita(citas.IdCita);
                        foreach (ClaseCitas inv in tratamientos)
                        {

                            citas.InsertarDetalleCita(citas.IdCita, inv.IdTratamiento, float.Parse(inv.trtamientoprecio));
                        }
                        btnGuardar.IsEnabled = false;
                        btnEditar.IsEnabled = true;
                        btnAgregar.IsEnabled = true;
                        btnCancelar.IsEnabled = false;
                        btnelimtratamiento.IsEnabled = false;
                        dtg_Citas.IsEnabled = true;
                        btnelimtratamiento_cita_Copy.IsEnabled = true;

                        MessageBox.Show("Exito al editar");
                        limpiar();
                        encenapagarbotones();
                        mostrarCitas();

                    }
                }


                else
                {
                    if (RevisarDatos())
                    {
                        ObtenerValores();


                        citas.AgendarCita(citas);
                        foreach (ClaseCitas inv in tratamientos)
                        {

                            citas.InsertarDetalleCita(citas.IdCita, inv.IdTratamiento, float.Parse(inv.trtamientoprecio));
                        }
                        MessageBox.Show("Cita agendada con éxito");
                        btnGuardar.IsEnabled = false;
                        btnEditar.IsEnabled = true;
                        btnAgregar.IsEnabled = true;
                        btnCancelar.IsEnabled = false;
                        btnelimtratamiento.IsEnabled = false;
                        dtg_Citas.IsEnabled = true;
                        btnelimtratamiento_cita_Copy.IsEnabled = true;
                        limpiar();
                        encenapagarbotones();
                        mostrarCitas();
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
             
               
            }
        }

        /// <summary>
        /// COn esta función el textbox para la fecha obtiene su valor basado en la selección del calendario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    

        private void dtg_Citas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val;
            if (dtg_Citas.SelectedValue != null)
            {

                cmbEmpleado.SelectedValue = ((ClaseCitas)dtg_Citas.SelectedItem).IdEmpleado;
                cmbPaciente.SelectedValue = ((ClaseCitas)dtg_Citas.SelectedItem).IdPacientes;
                cdCitas.SelectedDate = ((ClaseCitas)dtg_Citas.SelectedItem).fechaCita;
                ctTiempo.SelectedTime = ((ClaseCitas)dtg_Citas.SelectedItem).fechaCita;
                ctTiempo.SelectedTime = ((ClaseCitas)dtg_Citas.SelectedItem).fechaCita;
                val = ((ClaseCitas)dtg_Citas.SelectedItem).IdCita;

                dtg_Tratamientos.ItemsSource = citas.Mostrartratmientos(val);

                dtg_Tratamientos.SelectedValuePath = "IdCita";
                int c = 0;
                tratamientos.Clear();
                foreach (ClaseCitas tr in dtg_Tratamientos.ItemsSource)
                {
                    tratamientos.Add(new ClaseCitas
                    {
                        IdTratamiento = tr.IdTratamiento,
                        nombreTramientoindividual = tr.nombreTramientoindividual.ToString(),
                        trtamientoprecio = tr.trtamientoprecio.ToString(),
                        detalleCita = tr.detalleCita,
                        IdCita = val


                    });
                    if (c == 0)
                    {
                        cmbTratamiento.SelectedValue = ((ClaseCitas)tr).IdTratamiento;
                        c = 1;
                    }
                }

            }






        }

        /// <summary>
        /// COn el botón borrar se elimina del registro la cita agendada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

       

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dtg_Citas.IsEnabled = true;     
            btnAgregar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnelimtratamiento.IsEnabled = false;
            btnelimtratamiento_cita_Copy.IsEnabled= true;
            encenapagarbotones();
            limpiar();
            
           

        }

       

        private void btnelimtratamiento_Click_1(object sender, RoutedEventArgs e)
        {
            if (dtg_Tratamientos.SelectedValue != null)
            {
                tratamientos.RemoveAt(dtg_Tratamientos.SelectedIndex);
                dtg_Tratamientos.ItemsSource = tratamientos;
                dtg_Tratamientos.SelectedValuePath = "IdTratamiento";
               
            }
            else
            {
                MessageBox.Show("Seleccione un tratamiento para eliminarla");
            }

        }

        public void encenderbotones()
        {
            cmbEmpleado.IsEnabled = true;
            cmbTratamiento.IsEnabled = true;
            ctTiempo.IsEnabled = true;
            cdCitas.IsEnabled = true;
            cmbPaciente.IsEnabled = true;
            Agregar_Tratamientos.IsEnabled = true;

        }

        public void encenapagarbotones()
        {
            cmbEmpleado.IsEnabled = false;
            cmbTratamiento.IsEnabled = false;
            ctTiempo.IsEnabled = false;
            cdCitas.IsEnabled = false;
            cmbPaciente.IsEnabled = false;
            Agregar_Tratamientos.IsEnabled = false;
        }


        public void  limpiar()

          {
            tratamientos.Clear();
            
            cdCitas.SelectedDate = null;
            ctTiempo.SelectedTime = null;
            cmbEmpleado.SelectedValue = null;
            cmbPaciente.SelectedValue = null;
            cmbTratamiento.SelectedValue= null;
            dtg_Tratamientos.ItemsSource = tratamientos;
            dtg_Tratamientos.SelectedValuePath = "IdTratamiento";
            mostrarCitas();


        }

       

        

        private void btnelimtratamiento_cita_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtg_Citas.SelectedValue != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("¿Seguro que desea eliminar la cita?", "Eliminar Cita", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        obtenerCita();
                        citas.eliminardetallecita(citas.IdCita);
                        citas.EliminarCita(citas.IdCita);
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
                limpiar();
                mostrarCitas();
            }


        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuNavegacion.IsTopDrawerOpen = true;
        }

        private void MenuNavegacion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MenuNavegacion.IsTopDrawerOpen)
                MenuNavegacion.IsTopDrawerOpen = false;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void btnAgendarCita_Click(object sender, RoutedEventArgs e)
        {
            if (MenuNavegacion.IsTopDrawerOpen)
                MenuNavegacion.IsTopDrawerOpen = false;
        }

        private void btnRealizarCita_Click(object sender, RoutedEventArgs e)
        {
            RealizarCitaVista VistaRealizarCita = new RealizarCitaVista();
            VistaRealizarCita.CambioDeVistaPrincipal += CambioDeVistaPrincipal;
            CambioDeVista(VistaRealizarCita);
        }
    }

    
}
