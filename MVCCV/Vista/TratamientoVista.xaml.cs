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
        List<ClaseInventario> listMateriales = new List<ClaseInventario>();
        DataTable dtMaterial = new DataTable();

        public int gTratamientoID = 0;
        public bool isUpdate = false;
        public bool isListaUpdate = false;

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

        //START: Botones

            /*Tratamiento*/
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
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
                ObtenerMateriales();
            }
            CleanerLista();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            isUpdate = false;

            CamposEstado(false);

            btnAgregar.IsEnabled = false;
            btnEditar.IsEnabled = false;
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            dg_materiales.IsEnabled = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.Content = "Modificar";
            btnGuardar.IsEnabled = true;
            btnAgregar.IsEnabled = false;

            dg_tratamientos.IsEnabled = true;

            isUpdate = true;
            btnCancelar.IsEnabled = true;
            btnEditar.IsEnabled = false;   
            CamposEstado(false);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            btnListaCancelar.IsEnabled = false;
            Cleaner();
            isUpdate = false;
        }
            /*Lista de materiales de tratamiento*/
        private void btnListaAgregar_Click(object sender, RoutedEventArgs e)
        {
            if(ValidarListaDatos())
            {
                ClaseInventario mat = new ClaseInventario();

                if (!isListaUpdate)
                {
                    DataRowView dataRow = (DataRowView)dg_materiales.SelectedItem;

                    if (dataRow != null && !VerificarDatos(txtCantidad))
                    {
                        mat.IdMaterial = int.Parse(dataRow[0].ToString());
                        mat.NombreMaterial = dataRow[1].ToString();
                        mat.Cantidad = int.Parse(txtCantidad.Text);

                        listMateriales.Add(mat);
                        dg_materiales.Items.Refresh();
                        ObtenerMaterialFiltro();
                        dg_tratamientos_materiales.Items.Refresh();
                    }
                }
                else
                {
                    int index = dg_tratamientos_materiales.SelectedIndex;

                    listMateriales[index].Cantidad = int.Parse(txtCantidad.Text);
                    dg_tratamientos_materiales.Items.Refresh();

                    dg_materiales.SelectedIndex = -1;
                    dg_tratamientos_materiales.SelectedIndex = -1;

                    dg_tratamientos_materiales.Items.Refresh();
                    dg_materiales.Items.Refresh();
                }

                btnListaAgregar.IsEnabled = false;
                btnListaCancelar.IsEnabled = false;
                btnListaEliminar.IsEnabled = false;
                txtCantidad.Clear();
            }
        }

        private void btnListaEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dg_tratamientos_materiales.SelectedValue != null)
            {
                listMateriales.RemoveAt(dg_tratamientos_materiales.SelectedIndex);
                ObtenerMaterialFiltro();
                dg_tratamientos_materiales.Items.Refresh();

                btnListaEliminar.IsEnabled = false;
                btnListaCancelar.IsEnabled = false;
                btnListaAgregar.Content = "Agregar material";
                isListaUpdate = false;

                txtCantidad.Clear();
            }
        }

        private void btnListaCancelar_Click(object sender, RoutedEventArgs e)
        {
            CleanerLista();
        }

        //END: Botones

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
                ClaseInventario mat = new ClaseInventario();
                mat.IdMaterial = int.Parse(row["InventarioID"].ToString());
                mat.NombreMaterial = row["Nombre"].ToString();
                mat.Cantidad = int.Parse(row["CantidadUsada"].ToString());
 
                listMateriales.Add(mat);
            }

            dg_tratamientos_materiales.Items.Refresh();
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
                proc.InsertarTratamientoDetalle(tratID, item.IdMaterial, item.Cantidad);
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
            dtMaterial = proc.ObtenerMaterialesDatos();
            dg_materiales.ItemsSource = dtMaterial.DefaultView;

            dg_materiales.SelectedValuePath = "InvID";
        }

        private void ObtenerMaterialFiltro()
        {
            string filtro = "0";

            DataTable dt = new DataTable();

            dt = dtMaterial;

            for(int index = 0; index < listMateriales.Count; index++)
            {
                if(index == 0)
                {
                    filtro = listMateriales[index].IdMaterial.ToString();
                }
                else
                {
                    filtro += string.Join(",", "," + listMateriales[index].IdMaterial);
                }
            }

            string where = "InventarioID NOT IN ("+ filtro +")";
            string order = "Nombre ASC";
            dt = dt.Select(where, order).CopyToDataTable();

            dg_materiales.ItemsSource = dt.DefaultView;

            dg_materiales.SelectedValuePath = "InvID";
        }

        private void LlenarMaterialSelecionado()
        {
            dg_tratamientos_materiales.ItemsSource = listMateriales;
            dg_materiales.SelectedValuePath = "InvID";
        }

        //START: DGS - Cambio de selecion

        /*Lista Tratamiento*/
        private void dg_tratamientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView dataRow = (DataRowView)dg_tratamientos.SelectedItem;
            if (dataRow != null)
            {
                gTratamientoID = int.Parse(dataRow[0].ToString());

                dg_materiales.IsEnabled = true;
                
                ObtenerTratamientoDatos();
                ObtenerInventarioTratamientos(gTratamientoID);
                ObtenerMaterialFiltro();
            }
        }

            /*Lista Materiales*/
        private void dg_materiales_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if(dg_materiales.SelectedIndex != -1)
            {
                dg_tratamientos_materiales.SelectedIndex = -1;
                dg_tratamientos_materiales.Items.Refresh();
                btnListaAgregar.IsEnabled = true;
                btnListaCancelar.IsEnabled = true;
                btnListaEliminar.IsEnabled = false;
                isListaUpdate = false;

                btnListaAgregar.Content = "Agregar material";
                txtCantidad.Clear();
            }
        }

        private void dg_tratamientos_materiales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dg_tratamientos_materiales.SelectedIndex != -1)
            {
                dg_materiales.SelectedIndex = -1;
                btnListaEliminar.IsEnabled = true;
                btnListaCancelar.IsEnabled = true;

                btnListaAgregar.Content = "Modificar material";
                btnListaAgregar.IsEnabled = true;

                isListaUpdate = true;

                int index = dg_tratamientos_materiales.SelectedIndex;

                txtCantidad.Text = listMateriales[index].Cantidad.ToString();
            }
        }
        //END: DGS - Cambio de selecion

        private void Cleaner()
        {
            txtTratamientoNombre.Clear();
            txtPrecioSugerido.Clear();
            txtCantidad.Clear();
            chkestado.IsChecked = true;
            chkmasUno.IsChecked = false;
            dg_materiales.IsEnabled = true;

            listMateriales.Clear();
            dg_tratamientos_materiales.Items.Refresh();

            dg_tratamientos.IsEnabled = true;
            dg_materiales.IsEnabled = false;

            dg_tratamientos.SelectedIndex = -1;
            dg_tratamientos_materiales.SelectedIndex = -1;
            dg_materiales.SelectedIndex = -1;

            btnGuardar.Content = "Guardar";
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnEditar.IsEnabled = true;
            btnAgregar.IsEnabled = true;

            btnListaAgregar.Content = "Agregar material";

            btnListaEliminar.IsEnabled = false;
            btnListaAgregar.IsEnabled = false;

            dg_tratamientos.IsEnabled = false;
            dg_materiales.IsEnabled = false;

            listMateriales.Clear();

            CamposEstado(true);
            
            isListaUpdate = false;
        }

        private void CleanerLista()
        {
            dg_tratamientos_materiales.SelectedIndex = -1;
            dg_materiales.SelectedIndex = -1;

            txtCantidad.Clear();
            btnListaAgregar.Content = "Agregar material";
            btnListaAgregar.IsEnabled = false;
            btnListaEliminar.IsEnabled = false;
            btnListaCancelar.IsEnabled = false;

            isListaUpdate = false;
        }
        private void CamposEstado(bool estado)
        {
            txtTratamientoNombre.IsEnabled = !estado;
            txtPrecioSugerido.IsEnabled = !estado;
            txtCantidad.IsEnabled = !estado;
            chkestado.IsEnabled = !estado;
            chkmasUno.IsEnabled = !estado;
        }

        private bool ValidarDatos()
        {
            if (VerificarDatos(txtTratamientoNombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre al tratamiento.");
                return false;
            }

            if (VerificarDatos(txtPrecioSugerido))
            {
                MessageBox.Show("Por favor, ingrese un precio valido para el tratamiento.");
                return false;
            }

            if (!listMateriales.Any())
            {
                MessageBox.Show("Por favor, ingrese los materiales que el tratamiento requiere.");
                return false;
            }

            return true;
        }

        private bool ValidarListaDatos()
        {
            if (VerificarDatos(txtTratamientoNombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre al tratamiento.");
                return false;
            }

            if (VerificarDatos(txtPrecioSugerido) && validar.VerificarCantidad(double.Parse(txtPrecioSugerido.Text)))
            {
                MessageBox.Show("Por favor, ingrese un precio valido para el tratamiento.");
                return false;
            }

            if(VerificarDatos(txtCantidad))
            {
                MessageBox.Show("Por favor, ingrese la cantidad del material que usaria el tratamiento.");
            }

            return true;
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

        private void dg_materiales_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void dg_tratamientos_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
