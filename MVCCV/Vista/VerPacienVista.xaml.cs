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
        
        public ClasePaciente unPaciente = new ClasePaciente();

        private bool Admin;
        private String Nombree;

        /// <summary>
        /// Componentes que se inicializan junto al form
        /// </summary>
        public VerPacienVista()
        {
            InitializeComponent();
            MostrarPacientes();
            HabilitacionDeshabilitacion(false, true);
            dtpFechaNac.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.MaxValue));
        }

        public VerPacienVista(bool admin, string name)
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

            // txtEdad.IsEnabled = habilitacionGrupoA;
            cmbGenero.IsEnabled = habilitacionGrupoA;
            dtpFechaNac.IsEnabled = habilitacionGrupoA;
            btnEditarPaciente.IsEnabled = habilitacionGrupoB;
            cmbPaciente.IsEnabled = habilitacionGrupoB;
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
            txtEdad.Text = null;
            dtgHistorial.ItemsSource = null;
            cmbPaciente.SelectedValue = null;
            dtpFechaNac.SelectedDate = null;
        }

        public void obtenerValores()
        {
            unPaciente.NombrePaciente = txtNombre.Text;
            unPaciente.ApellidoPaciente = txtApellido.Text;
            unPaciente.FechaNac = Convert.ToDateTime(dtpFechaNac.Text);
            unPaciente.Genero = cmbGenero.Text;
            unPaciente.Telefono = txtTelefono.Text;
            unPaciente.Id_paciente = txtIdentidad.Text;
            unPaciente.Fecha = (DateTime)dtpFechaNac.SelectedDate;

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
            txtEdad.Visibility = Visibility;
            dtpFechaNac.Visibility = Visibility.Hidden;
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
        /// 
        private void btnGuardarPaciente_Click(object sender, RoutedEventArgs e)
        {
            // txtEdad.Visibility = Visibility;
            // dtpFechaNac.Visibility = Visibility.Hidden;
            try
            {
                if (VerificarCampos())
                {
                    obtenerValores();
                    unPaciente.ActualizarDatosPaciente(unPaciente);
                    MessageBox.Show("Éxito al actualizar los datos");
                    LimpiarPantalla();
                    MostrarPacientes();
                    HabilitacionDeshabilitacion(false, true);
                }
                else throw new Exception();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar al paciente");
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




        private bool VerificarCampos()
        {
            bool band = true;
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                switch (tb.Name)
                {
                    case "txtTelefono":
                        {
                            if (int.Parse(tb.Text) <= 100000000 && int.Parse(tb.Text) >= 99999999)
                                band = false;
                            break;
                        }

                    /*    case "txtEdad":
                            {
                                if (int.Parse(tb.Text) <= 0 && int.Parse(tb.Text) >= 110)
                                    band = false;
                                break;
                            }*/
                    case "txtIdentidad":
                        {
                            if (tb.Text.CompareTo("0101192100000") < 0 && tb.Text.CompareTo("1811202199999") > 0)
                                band = false;
                            break;
                        }
                    default:
                        {
                            if (tb.Text.Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox")
                                band = false;
                            break;
                        }
                }


            }
            return band;
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void AddPaciente_Closed(object sender, EventArgs e)
        {
            Pacientes pacientes = new Pacientes(Admin, Nombree);
            pacientes.Show();
        
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex reg = new Regex("[0-9]");
            bool b = reg.IsMatch(txtNombre.Text);
            if (b == true)
            {
                MessageBox.Show("Ingrese solamente caracteres");
                txtNombre.Text = "";
            }

        }

        private void AddPaciente_Loaded(object sender, RoutedEventArgs e)
        {
            Regex reg = new Regex("[0-9]");
            bool b = reg.IsMatch(txtNombre.Text);
            if (b == false)
            {

            }
        }

        private void dtgHistorial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
