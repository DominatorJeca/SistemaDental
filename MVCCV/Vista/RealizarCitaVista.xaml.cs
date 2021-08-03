﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RealizarCitaVista.xaml
    /// </summary>
    public partial class RealizarCitaVista : UserControl
    {
        ICollectionView Citas;
        ClaseProcedimiento proc = new ClaseProcedimiento();
        List<ClaseTratamiento> tratamientos = new List<ClaseTratamiento>();
        float total=0;
        public event EventHandler CambioDeVistaPrincipal;
        Validaciones val = new Validaciones();
        public Usuario user = new Usuario();
        public RealizarCitaVista()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarDatos();
        }
        protected virtual void CambioDeVista(object o)
        {
            if (MenuNavegacion.IsTopDrawerOpen)
                MenuNavegacion.IsTopDrawerOpen = false;
            if (CambioDeVistaPrincipal != null)
                CambioDeVistaPrincipal(o, null);
        }
        private void MostrarDatos()
        {
            cmbTratamientos.ItemsSource = null;
            dgvCitas.SelectedValuePath = "IdCita";
            Citas = new CollectionViewSource() { Source = proc.MostrarCitasHoy() }.View;
            dgvCitas.ItemsSource = Citas;
            cmbPaciente.SelectedValuePath = "Id_paciente";
            cmbPaciente.DisplayMemberPath = "NombrePaciente";
            cmbPaciente.ItemsSource = proc.MostrarPacientesAct();
   
            dgvCitas.SelectedIndex = 0;

        }

        private void dgvCitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCitas.SelectedValue != null)
            {
                total = 0;
                tratamientos = proc.MostrarTratamientoCitas((int)dgvCitas.SelectedValue);
                cmbTratamientos.SelectedValuePath = "IdTratamiento";
                cmbTratamientos.DisplayMemberPath = "NombreTratamiento";
                cmbMaterial.SelectedValuePath = "IdMaterial";
                cmbMaterial.DisplayMemberPath = "NombreMaterial";
               
                foreach (ClaseTratamiento trat in tratamientos )
                {
                    trat.Materiales = proc.MostrarInventarioxTratamiento(trat.IdTratamiento);
                    total += trat.precioTrat;
                }
                total += (total * (float)0.15);
                cmbTratamientos.ItemsSource = tratamientos;
                txtTotal.Text = total + "";
                cmbTratamientos.SelectedIndex = 0;
       
            }
            else
            {
                cmbTratamientos.SelectedIndex = -1;
                cmbTratamientos.ItemsSource = null;
                tratamientos = null;
            }
        }

        private void cmbTratamientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTratamientos.SelectedIndex < 0)
            {
                cmbMaterial.ItemsSource = null;
                cmbMaterial.SelectedIndex = -1;
            }
            else
            {
                cmbMaterial.ItemsSource = ((ClaseTratamiento)cmbTratamientos.SelectedItem).Materiales;
                cmbMaterial.SelectedIndex = 0;
            }
         
         
        }

        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPaciente.SelectedIndex>=0)
            {
                var filtro = new Predicate<object>(item => ((ClaseCitas)item).IdPacientes.ToString() == cmbPaciente.SelectedValue.ToString());
                Citas.Filter = filtro;
                dgvCitas.ItemsSource = Citas;
            }
            else
            {
         
                Citas.Filter = null;
                dgvCitas.ItemsSource = Citas;
            }
        }

        private void txtCantidadCobrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cmbTratamientos.SelectedItem != null && txtCantidadCobrada.Text != "")
            ((ClaseTratamiento)cmbTratamientos.SelectedItem).precioTrat = float.Parse(txtCantidadCobrada.Text);
            total = 0;
            foreach (ClaseTratamiento trat in tratamientos)
            {
                total += trat.precioTrat;
               
            }
            total += total * (float)0.15;
            txtTotal.Text = total + "";
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {

            if (!val.VerificarCampos(this))
            {
                MessageBox.Show("Porfavor llene todos los campos necesarios");
                return;
            }    
            if (val.VerificarCantidad(Convert.ToDouble(txtCantidadCobrada.Text)) && !val.VerificarCantidad(Convert.ToDouble(txtCantidadMaterial.Text)))
            {
                MessageBox.Show("Porfavor ingrese cantidades mayores a 0");
                return;
            }

            foreach (ClaseTratamiento trat in tratamientos)
            {
               if (!val.VerificarCantidad(trat.precioTrat))
                {
                    MessageBox.Show("El tratamiento "+trat.NombreTratamiento+" tiene como precio 0 porfavor corrija este error");
                    cmbTratamientos.SelectedItem = trat;
                    return;
                }
                foreach (ClaseInventario inv in trat.Materiales)
                    if (!val.VerificarCantidad(inv.Cantidad))
                    {
                        MessageBox.Show("El Material " + inv.NombreMaterial+ " en el tratamiento"+ trat.NombreTratamiento+" tiene como cantidad 0 porfavor corrija este error");
                        cmbTratamientos.SelectedItem = trat;
                        return;
                    }
            }
            proc.FinalizarCita((ClaseCitas)dgvCitas.SelectedItem);
           foreach (ClaseTratamiento trat in tratamientos)
            {
                proc.ActualizarDetallesCita(trat);
                foreach (ClaseInventario inv in trat.Materiales)
                    proc.RestarMaterial(inv);
            }
            proc.InsertarLog(user.Ide, "Finalizó una cita");
            MostrarDatos();
            MessageBox.Show("Cita realizada con exito");
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

        private void btnRealizarCita_Click(object sender, RoutedEventArgs e)
        {
            if (MenuNavegacion.IsTopDrawerOpen)
                MenuNavegacion.IsTopDrawerOpen = false;
        }

        private void btnAgendarCita_Click(object sender, RoutedEventArgs e)
        {
        CitaVista VistaAgendarCita = new CitaVista();
            VistaAgendarCita.CambioDeVistaPrincipal += CambioDeVistaPrincipal;
        CambioDeVista(VistaAgendarCita);
        }

        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            val.SoloNumeros(e);
        }

        private void SoloNumerosDec(object sender, TextCompositionEventArgs e)
        {
            val.SoloNumerosDec(e);
        }
    }
    }

