﻿using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para VistaAgregarUsuario.xaml
    /// </summary>
    public partial class VistaAgregarUsuario : UserControl
    {
     
        //Variables miembro
        private Usuario usuario = new Usuario();
        private Puesto puesto = new Puesto();
        private bool Admin;
        private String Nombree;

        //Contructores
        public VistaAgregarUsuario()
        {
            InitializeComponent();
            MostrarPuesto();

        }

        public VistaAgregarUsuario(bool admin, string name)
        {
            InitializeComponent();
            MostrarPuesto();
            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Abre el formulario ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

        /// <summary>
        /// Asigna la lista de puestos al cmbPuestos
        /// </summary>
        public void MostrarPuesto()
        {
            cmbPuesto.ItemsSource = puesto.MostrarPuestos();
            cmbPuesto.SelectedValuePath = "Id";
            cmbPuesto.DisplayMemberPath = "NombrePuesto";
        }

        /// <summary>
        /// Obtiene los datos de los textbox y combobox en el formulario
        /// </summary>
        public void ObtenerValores()
        {
            usuario.Id = Convert.ToInt32(txtAgregarIdentidad.Text);
            usuario.Nombre = Convert.ToString(txtAgregarNombre.Text);
            usuario.Apellido = Convert.ToString(txtAgregarApellido.Text);
            usuario.Telefono = Convert.ToString(txtAgregarTelefono.Text);
            usuario.Correo = Convert.ToString(txtAgregarCorreo.Text);
            usuario.Puesto = Convert.ToInt32(cmbPuesto.SelectedValue);
            usuario.Genero = ((ComboBoxItem)cmbSexo.SelectedItem).Content.ToString();
            usuario.Contraseña = txtAgregarContra.Password;
            usuario.Estado = true;
            usuario.Administrador = false;
        }


        /// <summary>
        /// Verifica que los valores de los textbox y combobox no esten vacios
        /// </summary>
        /// <returns>Verificacion de valores</returns>
        /*private bool VerificarValores()
        {
            if (txtAgregarApellido.Text == string.Empty || txtAgregarNombre.Text == string.Empty || txtAgregarCorreo.Text == string.Empty
                || txtAgregarIdentidad.Text == string.Empty || txtAgregarTelefono.Text == string.Empty || txtAgregarContra.Password == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            /*else if (cmbSexo.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el Sexo del nuevo empleado");
                return false;
            }
            else if (cmbPuesto.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el puesto del nuevo empleado");
                return false;
            }
            else if (txtConfirmarContra.Password != txtAgregarContra.Password)
            {
                MessageBox.Show("La confirmación de contraseña no coincide");
                return false;
            }
            else if (!ValidarEmail(txtConfirmarCorreo.Text) && !ValidarEmail(txtAgregarCorreo.Text))
            {
                MessageBox.Show("Por favor, ingrese un correo valido");
                return false;
            }
            else if (txtConfirmarCorreo.Text != txtAgregarCorreo.Text)
            {
                MessageBox.Show("La confirmaciónn de correo no coincide");
                return false;
            }


         return true;
     }*/

        /// <summary>
        /// Funcion para limpiar los textbox y combobox del formulario
        /// </summary>
        private void LimpiarFormulario()
        {
            txtAgregarApellido.Text = string.Empty;
            txtAgregarNombre.Text = string.Empty;
            txtAgregarCorreo.Text = string.Empty;
            txtAgregarIdentidad.Text = string.Empty;
            txtAgregarTelefono.Text = string.Empty;
            txtAgregarContra.Password = string.Empty;
            txtConfirmarContra.Password = string.Empty;
            txtConfirmarCorreo.Text = string.Empty;
            cmbPuesto.SelectedValue = null;
            cmbSexo.SelectedValue = null;

        }

        /// <summary>
        /// Lllama al metodo de agregar usuario y envia los parametros correspondientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            
                try
                {
                    // Obtener los valores para el empleado
                    ObtenerValores();

                    // Insertar los datos del usuario 
                    usuario.IngresarUsuario(usuario);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos insertados correctamente!");
                    LimpiarFormulario();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar el empleado...");
                    Console.WriteLine(ex.Message);
                }

            }

     

        private void PreviewTextInputOnlyLetters(object sender, TextCompositionEventArgs e)
        {

            validar.SoloLetras(e);

        }


        private void PreviewTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {

            validar.SoloNumeros(e);
        }


    }
}

