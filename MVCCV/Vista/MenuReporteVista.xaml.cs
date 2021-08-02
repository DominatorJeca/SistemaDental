using System;
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
using ControlzEx.Standard;
using Microsoft.Reporting.WinForms;


namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Interaction logic for MenuReporteVista.xaml
    /// </summary>
    public partial class MenuReporteVista : UserControl
    {
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        int reporteseleccionado = 0;
        DataTable dt;
        ReportDataSource ds;
        public Usuario user;
        public MenuReporteVista()
        {
            InitializeComponent();
            ManejoReportes();
            cmbEmpleado.ItemsSource = procedimiento.NombreEmpleados();
            cmbEmpleado.SelectedValuePath = "Empleado";
            cmbEmpleado.DisplayMemberPath = "Empleado";
        }

        
        public void ManejoReportes()
        {
            txtanio.SelectedValue = "";
            Mes.SelectedValue = "";
            reporte.Clear();
            foreach(Control control in stpPanel.Children)
            {
                control.Visibility = Visibility.Collapsed;
            }

           
        }

        private int mes()
        {
            switch (Mes.Text)
            {
                case "Enero":
                    return 1;
                case "Febrero":
                    return 2;
                case "Marzo":
                    return 3;
                case "Abril":
                    return 4;
                case "Mayo":
                    return 5;
                case "Junio":
                    return 6;
                case "Julio":
                    return 7;
                case "Agosto":
                    return 8;
                case "Septiembre":
                    return 9;
                case "Octubre":
                    return 10;
                case "Noviembre":
                    return 11;
                case "Diciembre":
                    return 12;
                case "Todos":
                    return 0;
            }
            return 0;
        }

        private void ObtenerValores()
        {
            procedimiento.anio = Convert.ToInt32(txtanio.Text);
            procedimiento.mes = mes();
            procedimiento.tratamiento = Convert.ToString(Tratamiento.SelectedValue);
            procedimiento.NombreEmpleado = Convert.ToString(cmbEmpleado.SelectedValue);
        }

        private void btnreporte1_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 1;
            ManejoReportes();
            ManejoVisibilidad();
        }

        private void btnreporte2_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 2;
            ManejoReportes();
            ManejoVisibilidad();
        }

        private void btnreporte3_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 3;
            ManejoReportes();
            ManejoVisibilidad();
            Tratamiento.ItemsSource = procedimiento.NombreTratamientos();
            Tratamiento.SelectedValuePath = "Nombre";
            Tratamiento.DisplayMemberPath = "Nombre";
        }

        private void btnreporte4_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 4;
            ManejoReportes();
            ManejoVisibilidad();
        }

        private void btnreporte5_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 5;
            ManejoReportes();
            ManejoVisibilidad();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            switch (reporteseleccionado)
            {
                case 1:
                    ObtenerValores();
                    reporte.Reset();
                    dt = procedimiento.FechaVenc();
                    ds = new ReportDataSource("DataSet1", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.Report1.rdlc";
                    reporte.RefreshReport();
                    break;
                case 2:
                    ObtenerValores();
                    reporte.Reset();
                    dt = procedimiento.TotalCompraMes();
                    ds = new ReportDataSource("DataSetMesCompra", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.CompraMes.rdlc";
                    reporte.RefreshReport();
                    break;
                case 3:
                    ObtenerValores();
                    reporte.Reset();
                    dt = procedimiento.TotalxTratamiento();
                    ds = new ReportDataSource("DataSettt", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportTratamiento.rdlc";
                    reporte.RefreshReport();
                    break;
                case 4:
                    ObtenerValores();
                    reporte.Reset();
                    dt = procedimiento.VentasMes();
                    ds = new ReportDataSource("DataSetVxM", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportVentasxMes.rdlc";
                    reporte.RefreshReport();
                    break;
                case 5:
                    procedimiento.anio = Convert.ToInt32(txtanio.Text);
                    reporte.Reset();
                    dt = procedimiento.AnioGanancias();
                    ds = new ReportDataSource("DataSetAG", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportAnioGanancias.rdlc";
                    reporte.RefreshReport();
                    break;
                case 6:
                    procedimiento.NombreEmpleado = Convert.ToString(cmbEmpleado.SelectedValue);
                    reporte.Reset();
                    dt = procedimiento.TurnosxEmpleado();
                    ds = new ReportDataSource("DataSetTE", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportTurnosxEmpleado.rdlc";
                    reporte.RefreshReport();
                    break;
                case 7:
                    procedimiento.NombreEmpleado = Convert.ToString(cmbEmpleado.SelectedValue);
                    reporte.Reset();
                    dt = procedimiento.Citasporempleado();
                    ds = new ReportDataSource("DataSetCitasEmpleado", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportCitasxEmpleado.rdlc";
                    reporte.RefreshReport();
                    break;
                case 8:
                    procedimiento.NombreEmpleado = Convert.ToString(cmbEmpleado.SelectedValue);
                    reporte.Reset();
                    dt = procedimiento.MostrarLog();
                    ds = new ReportDataSource("DataSetLog", dt);
                    reporte.LocalReport.DataSources.Add(ds);
                    reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportLog.rdlc";
                    reporte.RefreshReport();
                    break;
            }
        }

        private void btnreporte6_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 6;
            ManejoReportes();
            ManejoVisibilidad();
            
        }


        private void ManejoVisibilidad()
        {
            btnMostrarReporte.Visibility = Visibility.Visible;
            if(reporteseleccionado==1 || reporteseleccionado == 2 || reporteseleccionado == 3 || reporteseleccionado == 4)
            {
                lblanio.Visibility = Visibility.Visible;
                txtanio.Visibility = Visibility.Visible;
                lblmes.Visibility= Visibility.Visible;
                Mes.Visibility= Visibility.Visible;
            }
            if (reporteseleccionado == 5)
            {
                lblanio.Visibility = Visibility.Visible;
                txtanio.Visibility = Visibility.Visible;
            }
            if (reporteseleccionado == 3)
            {
                lbltratamiento.Visibility = Visibility.Visible;
                Tratamiento.Visibility = Visibility.Visible;
            }
            if (reporteseleccionado == 6 || reporteseleccionado==7|| reporteseleccionado==8)
            {
                lblempleado.Visibility = Visibility.Visible;
                cmbEmpleado.Visibility = Visibility.Visible;
            }
        }

        private void btnreporte7_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 7;
            ManejoReportes();
            ManejoVisibilidad();

        }

        private void btnreporte8_Click(object sender, RoutedEventArgs e)
        {
            reporteseleccionado = 8;
            ManejoReportes();
            ManejoVisibilidad();
            procedimiento.NombreEmpleado = "";
            reporte.Reset();
            dt = procedimiento.MostrarLog();
            ds = new ReportDataSource("DataSetLogTodos", dt);
            reporte.LocalReport.DataSources.Add(ds);
            reporte.LocalReport.ReportEmbeddedResource = "SistemaDental.ReportLogTodos.rdlc";
            reporte.RefreshReport();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            permisos();
        }

        private void permisos()
        {
            if (!user.Administrador)
            {
                btnreporte7.IsEnabled = false;
                btnreporte8.IsEnabled = false;
                btnreporte6.IsEnabled = false;

            }
        }
    }
}
