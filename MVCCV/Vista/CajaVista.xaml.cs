﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para CajaVista.xaml
    /// </summary>
    public partial class CajaVista : UserControl
    {
        private bool bandera;
        private bool Admin;
        private String Nombree;
        private ClaseCaja caja = new ClaseCaja();
        private ClaseProcedimiento proc = new ClaseProcedimiento();
        ICollectionView ColeccionTransacciones;


        public CajaVista()
        {
            InitializeComponent();
            ObtenerValues();
        }

        public CajaVista(bool admin, string name)
        {
            InitializeComponent();
           
            Nombree = name;
            Admin = admin;

        }

        public void MostrarCaja()
        {

           /* dgvCaja.ItemsSource = proc.traerTransaccionesCitas();*/

        }

        private void ObtenerValues()
        {
            cmbPaciente.SelectedValuePath = "Id_paciente";
            cmbPaciente.DisplayMemberPath = "NombrePaciente";
            cmbPaciente.ItemsSource = proc.MostrarPacientesAct();
         
          
        }


        /// <summary>
        /// Verifica que todos los valores no esten vacios
        /// </summary>


        private void dgvCaja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCaja.SelectedValuePath !="" && dgvCaja.SelectedValue!=null) 
            MessageBox.Show(dgvCaja.SelectedValue.ToString());
        }


        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            proc.InsertarTransaccion(37, float.Parse(txtDineroAbono.Text), txtObservaciones.Text, Convert.ToInt32(dgvCaja.SelectedValue));
            cmbPaciente_SelectionChanged(null, null);
        }

        private void cmbPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgvCaja.ItemsSource = proc.traerTransaccionesCitas()
                /* MessageBox.Show("Hola");*/
                ColeccionTransacciones = new CollectionViewSource() { Source = proc.traerTransaccionesCitas(Convert.ToInt32(cmbPaciente.SelectedValue)) }.View;
            dgvCaja.ItemsSource = ColeccionTransacciones;
            dgvCaja.SelectedValuePath = "cita.IdCita ";
        }

        private void dtpFechadeCita_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechadeCita.SelectedDate.ToString() == "")
            {
                ColeccionTransacciones.Filter = null;
                dgvCaja.ItemsSource = ColeccionTransacciones;
            }
            else
            {
                var filtro = new Predicate<object>(item => ((ClaseCaja)item).cita.fechaCita == dtpFechadeCita.SelectedDate);
                ColeccionTransacciones.Filter = filtro;
                dgvCaja.ItemsSource = ColeccionTransacciones;
            }
        }
    }
}
