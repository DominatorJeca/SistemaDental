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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para VerPacienVista.xaml
    /// </summary>
    public partial class VerPacienVista : UserControl
    {
        public bool guardaropc;
        public Usuario usuar = new Usuario();
        public ClasePaciente unPaciente = new ClasePaciente();
        private ClaseProcedimiento Proc = new ClaseProcedimiento();
        private bool Admin;
        private String Nombree;
        Validaciones validar = new Validaciones();

        /// <summary>
        /// Componentes que se inicializan junto al form
        /// </summary>
        public VerPacienVista()
        {
            InitializeComponent();
            MostrarPacientes();
            HabilitacionDeshabilitacion(false, false);
            cmbPaciente.IsEnabled = true;
            MostrarGenero();
            dtpFechaNac.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.MaxValue));
        }

        public VerPacienVista(bool admin, string name)
        {

            InitializeComponent();
            MostrarPacientes();
            HabilitacionDeshabilitacion(false, false);
            cmbPaciente.IsEnabled = true;
            MostrarGenero();
            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Boton para regresar al menu anterior
        /// </summary>


        /// <summary>
        /// Funcion para llenar el combobox con los id extraidos de la BD mediante la clase ClasePaciente
        /// </summary>
        public void MostrarPacientes()
        {
            cmbPaciente.ItemsSource = unPaciente.MostrarPacientes();
            cmbPaciente.SelectedValuePath = "Id_paciente";
            cmbPaciente.DisplayMemberPath = "Identidad";
        }

        public void MostrarGenero()
        {
            cmbGenero.ItemsSource = Proc.MostrarGenero();
            cmbGenero.SelectedValuePath = "Id";
            cmbGenero.DisplayMemberPath = "NombreGenero";
        }

        /// <summary>
        /// Funcion para habilitar o deshabilitar los botones, textbox y combobox del formulario
        /// </summary>
        ///

        public void HabilitacionDeshabilitacion(bool habilitacionGrupoA, bool habilitacionGrupoB)
        {
            //Grupo de datos
            txtNombre.IsEnabled = habilitacionGrupoA;
            txtApellido.IsEnabled = habilitacionGrupoA;
            txtTelefono.IsEnabled = habilitacionGrupoA;
            cmbGenero.IsEnabled = habilitacionGrupoA;
            txtcorreo.IsEnabled = habilitacionGrupoA;
            txtIdentidad.IsEnabled = habilitacionGrupoB;
            dtpFechaNac.IsEnabled = habilitacionGrupoB;
            cmbPaciente.IsEnabled = habilitacionGrupoB;


        }
        private bool VerificarIDRepetida(string identidad)
        {

            if (identidad == Proc.BuscarPaciente(identidad) && ((ClasePaciente)cmbPaciente.SelectedItem).Identidad != identidad)
            {
                MessageBox.Show("El numero de identidad ya pertenece a un paciente ingresado");
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Funcion para limpiar el contenido del Form
        /// </summary>
        public void LimpiarPantalla()
        {
            txtNombre.Text = null;
            txtApellido.Text = null;
            txtIdentidad.Text = null;
            txtTelefono.Text = null;
            dtgHistorial.ItemsSource = null;
            cmbPaciente.SelectedValue = null;
            cmbGenero.SelectedValue = null;
            dtpFechaNac.SelectedDate = null;
            txtcorreo.Text = null;
        }

        public void obtenerValores()
        {
            unPaciente.NombrePaciente = txtNombre.Text;
            unPaciente.ApellidoPaciente = txtApellido.Text;
            unPaciente.FechaNac = Convert.ToDateTime(dtpFechaNac.Text);

            unPaciente.Telefono = txtTelefono.Text;
            unPaciente.Identidad = txtIdentidad.Text;
            //unPaciente. = (DateTime)dtpFechaNac.SelectedDate;
            unPaciente.Genero = Convert.ToInt32(cmbGenero.SelectedValue);
            unPaciente.Correo = txtcorreo.Text;

        }

        /// <summary>
        /// Boton para editar el paciente seleccionado en el combobox de Id Paciente
        /// </summary>
        private void btnEditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            // txtEdad.Visibility = Visibility.Hidden;
            //dtpFechaNac.Visibility = Visibility;
         
            try
            {
                //Mensaje de advertencia si no selecciona ningun elemento
                if (cmbPaciente.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un paciente.");
                }
                else
                {
                    //Deshabilita el combobox paciente, el boton editar y ver
                    //Habilita los demás botones y textbox para poder editar los datos
                    guardaropc = false;
                    btnAgregarPaciente.IsEnabled = false;
                    btnRestablecerEstablecer.IsEnabled = false;
                    btnGuardarPaciente.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    btnEditarPaciente.IsEnabled = false;
                    
                    HabilitacionDeshabilitacion(true, false);
                }
            }
            catch
            { MessageBox.Show("Ha ocurrido un error en el sistema."); }
        }

        /// <summary>
        /// Boton para cancelar los cambios hechos y para limpiar la pantalla
        /// </summary>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //llama la funcion para deshabilitar los textbox y botones
                //llama la funcion para limpiar pantalla
                LimpiarPantalla();
                HabilitacionDeshabilitacion(false, false);
                MessageBox.Show("Se han cancelado los cambios.");
                btnEditarPaciente.IsEnabled = false;
                btnGuardarPaciente.IsEnabled = false;
                btnAgregarPaciente.IsEnabled = true;
                btnRestablecerEstablecer.IsEnabled = false;
                cmbPaciente.IsEnabled = true;
                btnCancelar.IsEnabled = false;
                //btnEliminar.IsEnabled = false;
            }

            catch { MessageBox.Show("Ha ocurrido un error en el sistema."); }
        }

        /// <summary>
        /// Boton Ver el historial del paciente seleccionado en el combobox
        /// </summary>
        ///
        private void btnGuardarPaciente_Click(object sender, RoutedEventArgs e)
        {
            // txtEdad.Visibility = Visibility;
            // dtpFechaNac.Visibility = Visibility.Hidden;


            if (guardaropc == false)
            {

                if (VerificarCampos() && VerificarIDRepetida(txtIdentidad.Text)==false)
                {
                    try
                    {

                        obtenerValores();
                        unPaciente.ActualizarDatosPaciente(unPaciente);
                        Proc.InsertarLog(usuar.Ide, "Se ha editado un Paciente");
                        MessageBox.Show("Éxito al editar los datos");
                        LimpiarPantalla();
                        MostrarPacientes();
                        HabilitacionDeshabilitacion(false, false);
                        cmbPaciente.IsEnabled = true;
                        btnAgregarPaciente.IsEnabled = false;
                        btnEditarPaciente.IsEnabled = false;
                        btnAgregarPaciente.IsEnabled = true;
                        btnGuardarPaciente.IsEnabled = false;
                        btnCancelar.IsEnabled = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al guardar al paciente");
                    }
                }
            }
            else
            {
                if (VerificarCampos() && VerificarIDRepetida(txtIdentidad.Text) == false)
                {
                    try
                    {

                        obtenerValores();
                        unPaciente.AgregarPaciente(unPaciente);
                        Proc.InsertarLog(usuar.Ide, "Se agregó un nuevo Paciente");
                        MessageBox.Show("Éxito al agregar los datos");
                        LimpiarPantalla();
                        MostrarPacientes();
                        HabilitacionDeshabilitacion(false, true);
                     
                       
                        btnEditarPaciente.IsEnabled = false;
                        btnAgregarPaciente.IsEnabled = true;
                        btnGuardarPaciente.IsEnabled = false;
                        btnCancelar.IsEnabled = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al guardar al paciente");
                    }
                }
            }
        }
    


        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPaciente.SelectedValue != null)
            {
                btnAgregarPaciente.IsEnabled = false;
                btnEditarPaciente.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnRestablecerEstablecer.Content = ((ClasePaciente)cmbPaciente.SelectedItem).Estado ? "Eliminar" : "Reestablecer";
                btnRestablecerEstablecer.IsEnabled = true;
                HabilitacionDeshabilitacion(false, false);
                dtgHistorial.IsEnabled = true;
                unPaciente.Id_paciente = int.Parse(cmbPaciente.SelectedValue.ToString());
                dtgHistorial.ItemsSource = unPaciente.MostrarHistorial(unPaciente);
            }


        }


        private bool VerificarCampos()
        {
            if (!validar.VerificarCampos(this))
            {
                MessageBox.Show("Por favor ingresa todos los valores que se le solicitan");


                return false;
            }
            if (!validar.VerificarIdentidad(txtIdentidad.Text))
            {
                MessageBox.Show("El numero de identidad no tiene un formato correcto");


                return false;
            }
            else if (!validar.VerificarNumero(txtTelefono.Text))
            {
                MessageBox.Show("El numero de telefono no tiene un formato correcto");


                return false;
            }

            else if (!validar.ValidarEmail(txtcorreo.Text))
            {

                MessageBox.Show("Por favor, ingrese un correo valido");

                return false;
            }

            return true;

        }

        private void AddPaciente_Closed(object sender, EventArgs e)
        {


        }

        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {


            validar.SoloNumeros(e);
        }

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {


            validar.SoloLetras(e);

        }



        private void dtgHistorial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
        
            if (cmbPaciente.SelectedItem != null)
            {
                MessageBox.Show("Por favor presione el boton cancelar");
            }
            else
            {
                guardaropc = true;
                btnAgregarPaciente.IsEnabled = false;
                HabilitacionDeshabilitacion(true, true);
                cmbPaciente.IsEnabled = false;
                dtpFechaNac.IsEnabled = true;
                btnEditarPaciente.IsEnabled = false;
                btnGuardarPaciente.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnRestablecerEstablecer.IsEnabled = false;
            }

    }





        //    

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPaciente.SelectedItem!=null && MessageBox.Show("Está seguro de realizar esta accion?","Aviso",MessageBoxButton.YesNoCancel)== MessageBoxResult.Yes)
            {
                ClasePaciente paciente = (ClasePaciente)cmbPaciente.SelectedItem;
                paciente.Estado = btnRestablecerEstablecer.Content.Equals("Reestablecer")? true:false;
                paciente.ActualizarDatosPaciente(paciente);
             
                LimpiarPantalla();
                MostrarPacientes();
                HabilitacionDeshabilitacion(false, false);
                if (paciente.Estado)
                {
                Proc.InsertarLog(usuar.Ide, "Se reestableció un paciente");
                    MessageBox.Show("Se ha reestablecido el paciente exitozamente");
                }

                else
                {

                 Proc.InsertarLog(usuar.Ide, "Se eliminó un paciente");
                 MessageBox.Show("Se ha eliminado el paciente exitosamente");
                }
                btnEditarPaciente.IsEnabled = false;
                btnGuardarPaciente.IsEnabled = false;
                btnAgregarPaciente.IsEnabled = true;
                btnRestablecerEstablecer.IsEnabled = false;
                btnCancelar.IsEnabled = false;
                cmbPaciente.IsEnabled = true;
            }
        }
    }
}
