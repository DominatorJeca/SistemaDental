﻿using System;
using System.Collections.Generic;
using System.Data;
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


        ClaseProcedimiento proc = new ClaseProcedimiento();
        ClaseTratamiento trat = new ClaseTratamiento();
        Validaciones validar = new Validaciones();
        List<Materiales> listMateriales = new List<Materiales>();

        public int gTratamientoID = 0;
        public bool isUpdate = false;

        private bool Admin;
        private String Nombree;
        public TratamientoVista()
        {
            InitializeComponent();
            ObtenerTratamientos();
            ObtenerMateriales();
            LlenarMaterialSelecionado();
        }
        public TratamientoVista(bool admin, string name)
        {
            InitializeComponent();

            Nombree = name;
            Admin = admin;
        }

        private void cmbTratamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {

           
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(dg_tratamientos.SelectedIndex != -1)
            {
                btnGuardar.Content = "Modificar";
                btnGuardar.IsEnabled = true;

                isUpdate = true;
                btnCancelar.IsEnabled = true;
                btnEditar.IsEnabled = false;   
                CamposEstado(false);
            }
            else
            {
                MessageBox.Show("Por favor, selecione el tratamiento a modificar en la lista de tratamientos.");
            }
        }

        private void ObtenerTratamientoDatos()
        {
            DataTable dt = new DataTable();

            DataRowView dataRow = (DataRowView)dg_tratamientos.SelectedItem;
            if (dataRow != null)
            {
                gTratamientoID = int.Parse(dataRow[0].ToString());

                dt = proc.BuscarTratamiento(gTratamientoID);
                foreach(DataRow row in dt.Rows)
                {
                    gTratamientoID = int.Parse(row["TratamientoID"].ToString());
                    txtTratamientoNombre.Text = row["Nombre"].ToString();
                    txtPrecioSugerido.Text = row["PrecioSugerido"].ToString();
                    chkestado.IsChecked = bool.Parse(row["Estado"].ToString());
                    chkmasUno.IsChecked = bool.Parse(row["MasUno"].ToString());
                }
            }
        }

        private void ObtenerInventarioTratamientos(int id)
        { 
            DataTable dt = new DataTable();

            listMateriales.Clear();

            dt = proc.BuscarInventarioTratamiento(id);
            foreach (DataRow row in dt.Rows)
            {
                Materiales mat = new Materiales();
                mat.InvID = int.Parse(row["InventarioID"].ToString());
                mat.NombreMaterial = row["Nombre"].ToString();
                mat.Cantidad = int.Parse(row["CantidadUsada"].ToString());
 
                listMateriales.Add(mat);
            }

            dg_tratamientos_materiales.Items.Refresh();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(ValidarDatos())
            {
                if (!isUpdate)
                {
                    GuardarTratamiento();
                }
                else
                {
                    ActualizarTratamiento();
                }

                MessageBox.Show("Datos insertados con exito");

                Cleaner();
                ObtenerTratamientos();
            }
        }

        private void ActualizarTratamiento()
        {
            proc.EliminarInventarioTratamiento(gTratamientoID);
            GuardarTratamientoInventario(gTratamientoID);

            trat.IdTratamiento = gTratamientoID;
            trat.TratamientoNombre = txtTratamientoNombre.Text;
            trat.precioSugerido = float.Parse(txtPrecioSugerido.Text);
            trat.Estado = ObtenerCheck(chkestado);
            trat.masUno = ObtenerCheck(chkmasUno);

            proc.ActualizarTratamiento(trat);

            dg_tratamientos.Items.Refresh();
        }

        private bool ValidarDatos()
        {
            if(VerificarDatos(txtTratamientoNombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre al tratamiento.");
                return false;
            }

            if(VerificarDatos(txtPrecioSugerido) && validar.VerificarCantidad(double.Parse(txtPrecioSugerido.Text)))
            {
                MessageBox.Show("Por favor, ingrese un precio valido para el tratamiento.");
                return false;
            }

            if(!listMateriales.Any())
            {
                MessageBox.Show("Por favor, los materiales que el tratamiento requiere.");
                return false;
            }

            return true;
        }

        private void GuardarTratamiento()
        {
            int id;

            trat.TratamientoNombre = txtTratamientoNombre.Text;
            trat.precioSugerido = float.Parse(txtPrecioSugerido.Text);
            trat.masUno = ObtenerCheck(chkmasUno);
            trat.Estado = ObtenerCheck(chkestado);

            id = proc.InsertarTratamiento(trat);

            GuardarTratamientoInventario(id);
        }

        private void GuardarTratamientoInventario(int tratID)
        {
            foreach(var item in listMateriales)
            {
                proc.InsertarTratamientoDetalle(tratID, item.InvID, item.Cantidad);
            }
        }

        private int ObtenerCheck(CheckBox chk)
        {
            if(chk.IsChecked.ToString() == "True")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CamposEstado(false);
            btnGuardar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            Cleaner();
            isUpdate = false;
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TratamientoWindow_Closed(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
        }

        private void ObtenerTratamientos()
        {
            dg_tratamientos.ItemsSource = proc.ObtenerTratamientosDatos().DefaultView;
            dg_tratamientos.SelectedValuePath = "TratamientoID";
        }

        private void ObtenerMateriales()
        {
            dg_materiales.ItemsSource = proc.ObtenerMaterialesDatos().DefaultView;
            dg_materiales.SelectedValuePath = "InvID";
        }

        private void dg_tratamientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CamposEstado(true);
            btnGuardar.IsEnabled = false;

            ObtenerTratamientoDatos();
            ObtenerInventarioTratamientos(gTratamientoID);

            DataRowView dataRow = (DataRowView)dg_tratamientos.SelectedItem;
            if (dataRow != null)
            {
                gTratamientoID = int.Parse(dataRow[0].ToString());
                ObtenerTratamientoDatos();
                ObtenerInventarioTratamientos(gTratamientoID);
            }
        }

        private void CamposEstado(bool estado)
        {
            txtTratamientoNombre.IsReadOnly = estado;
            txtPrecioSugerido.IsReadOnly = estado;
            chkestado.IsEnabled = !estado;
            chkmasUno.IsEnabled = !estado;
        }

        private void dg_materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnListaAgregar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dg_materiales.SelectedItem;
            Materiales mat = new Materiales();

            if (dataRow != null && !VerificarDatos(txtCantidad))
            {
                mat.InvID = int.Parse(dataRow[0].ToString());
                mat.NombreMaterial = dataRow[1].ToString();
                mat.Cantidad = int.Parse(txtCantidad.Text);

                listMateriales.Add(mat);
                dg_tratamientos_materiales.Items.Refresh();
                dg_materiales.SelectedIndex = -1;
            }
        }

        private void LlenarMaterialSelecionado()
        {
            dg_tratamientos_materiales.ItemsSource = listMateriales;
            dg_materiales.SelectedValuePath = "InvID";
        }

        private void btnListaEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(dg_tratamientos_materiales.SelectedValue != null)
            {
                listMateriales.RemoveAt(dg_tratamientos_materiales.SelectedIndex);
                dg_tratamientos_materiales.Items.Refresh();
            }
        }

        private void Cleaner()
        {
            txtTratamientoNombre.Clear();
            txtPrecioSugerido.Clear();
            chkestado.IsChecked = true;
            chkmasUno.IsChecked = false;
            txtCantidad.Clear();
            listMateriales.Clear();
            dg_tratamientos_materiales.Items.Refresh();

            dg_tratamientos.SelectedIndex = -1;
            dg_tratamientos_materiales.SelectedIndex = -1;
            dg_materiales.SelectedIndex = -1;

            btnGuardar.Content = "Guardar";
            btnGuardar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            btnListaEliminar.IsEnabled = false;

            listMateriales.Clear();
        }

        private void PreviewTextInputSoloLetras(object sender, TextCompositionEventArgs e)
        {
            validar.SoloLetras(e);
        }

        private void PreviewTextInputSoloNumeros(object sender, TextCompositionEventArgs e)
        {
            validar.SoloNumeros(e);
        }

        private bool VerificarDatos(TextBox input)
        {
            return string.IsNullOrEmpty(input.Text);
        }

        private void txtTratamientoNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dg_tratamientos_materiales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnListaEliminar.IsEnabled = true;
        }
    }

    public class Materiales
    {
        public int InvID { get; set; }
        public string NombreMaterial { get; set; }
        public int Cantidad { get; set; }

        public Materiales ()
        {

        }
    }
}
