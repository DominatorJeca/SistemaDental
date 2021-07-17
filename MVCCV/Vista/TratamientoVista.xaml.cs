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

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para TratamientoVista.xaml
    /// </summary>
    public partial class TratamientoVista : UserControl
    {
        

        ClaseTratamiento tratamiento = new ClaseTratamiento();
        DateTime DateTime = new DateTime();
        private bool Admin;
        private String Nombree;
        int cant_anterior = 0;
        public TratamientoVista()
        {
            InitializeComponent();
            MostrarTratamientos();
            MostrarPacientes();
        }
        public TratamientoVista(bool admin, string name)
        {
            InitializeComponent();
            MostrarTratamientos();
            MostrarPacientes();
            Nombree = name;
            Admin = admin;
        }

        public void MostrarTratamientos()
        {
            cmbTratamiento.ItemsSource = tratamiento.mostrarTratamientos();
            cmbTratamiento.DisplayMemberPath = "NombreTratamiento";
            cmbTratamiento.SelectedValuePath = "IdTratamiento";
        }

        public void MostrarPacientes()
        {
            cmbPaciente.ItemsSource = tratamiento.mostrarIdPacientes();
            cmbPaciente.DisplayMemberPath = "IdPaciente";
            cmbPaciente.SelectedValuePath = "IdPaciente";
        }

        public void MostrarMateriales()
        {
            dg_materiales.ItemsSource = tratamiento.mostrarMateriales(Convert.ToInt32(cmbTratamiento.SelectedValue));
            dg_materiales.SelectedValuePath = "NombreMaterial";
        }

        private void cmbTratamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarMateriales();
        }

        public void ObtenerValores()
        {
            tratamiento.fecha = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss");
            tratamiento.IdPaciente = Convert.ToString(cmbPaciente.SelectedValue);
            tratamiento.IdTratamiento = Convert.ToInt32(cmbTratamiento.SelectedValue);
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Convert.ToInt32(cmbTratamiento.SelectedValue) > 0 && Convert.ToString(cmbPaciente.SelectedValue) != " " && Convert.ToInt32(txtCantidad.ToString()) >= 0)
                {
                    ObtenerValores();
                    tratamiento.IngresarAlHistorial(tratamiento);
                    foreach (ClaseTratamiento tratamientos in dg_materiales.ItemsSource)
                    {
                        tratamiento.Cantidad = Convert.ToInt32(tratamientos.Cantidad.ToString());
                        tratamiento.NombreMaterial = tratamientos.NombreMaterial.ToString();
                        tratamiento.ActualizarMaterialDisponible(tratamiento);
                    }
                    MessageBox.Show("Ingreso al historial fue un éxito");
                }
                else
                {
                    MessageBox.Show("Asegurese de seleccionar un tratamiento y un paciente");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                cmbPaciente.SelectedItem = null;
                cmbTratamiento.SelectedItem = null;
            }

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            cant_anterior = Convert.ToInt32(txtCantidad.Text);
            try
            {
                if (dg_materiales.SelectedValue == null || cmbTratamiento.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un tratamiento y un material para poder ser editado");
                }
                else
                {

                    txtCantidad.IsEnabled = true;
                    btnGuardar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnRealizar.IsEnabled = false;
                    cmbTratamiento.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ClaseTratamiento tratamientos in dg_materiales.ItemsSource)
                {
                    if (tratamiento.NombreMaterial == dg_materiales.SelectedValue.ToString() && Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(tratamientos.Cantidad.ToString()))
                    {
                        tratamiento.Cantidad = Convert.ToInt32(tratamientos.Cantidad.ToString());

                    }
                    else
                    {
                        MessageBox.Show("Valor mayor a la cantidad actual");
                        txtCantidad.Text = cant_anterior.ToString();
                        break;
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                btnGuardar.IsEnabled = false;
                txtCantidad.IsEnabled = false;
                btnEditar.IsEnabled = true;
                btnRealizar.IsEnabled = true;
                cmbTratamiento.IsEnabled = true;
                btnCancelar.IsEnabled = false;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            txtCantidad.IsEnabled = false;
            btnEditar.IsEnabled = true;
            btnRealizar.IsEnabled = true;
            cmbTratamiento.IsEnabled = true;
            btnCancelar.IsEnabled = false;
        }

       

        private void dg_materiales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TratamientoWindow_Closed(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
        }

        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtMaterial_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
