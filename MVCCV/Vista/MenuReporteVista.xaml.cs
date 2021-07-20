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
using Microsoft.Reporting.WinForms;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Interaction logic for MenuReporteVista.xaml
    /// </summary>
    public partial class MenuReporteVista : UserControl
    {
        ClaseProcedimiento procedimiento = new ClaseProcedimiento();
        public MenuReporteVista()
        {
            InitializeComponent();
         
            
         
        }

        private void btnreporte1_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void btnreporte2_Click(object sender, RoutedEventArgs e)
        {
         
            /*rpViewer.ReportPath = null;
            rpViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\MesCompra.rdl");
            rpViewer.RefreshReport();*/
        }

        private void btnreporte3_Click(object sender, RoutedEventArgs e)
        {
       
            /*rpViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\tratamientos.rdl");
            rpViewer.Refresh();
            rpViewer.RefreshReport();*/
        }

        private void btnreporte4_Click(object sender, RoutedEventArgs e)
        {
          
           /* rpViewer.ReportPath = null;
            rpViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\VentasAñosoMesesEspecificos.rdl");
            rpViewer.Refresh();
            rpViewer.RefreshReport();*/
        }

        private void btnreporte5_Click(object sender, RoutedEventArgs e)
        {
           /* rpViewer.ReportPath = null;
            rpViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Reportes\VentasAñosoMesesEspecificos.rdl");
            rpViewer.Refresh();
            rpViewer.RefreshReport();*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            procedimiento.anio = Convert.ToInt32(txtanio.Text);
            procedimiento.mes = Convert.ToInt32(Mes.Text);
            reporte.Reset();
            DataTable dt = procedimiento.FechaVenc();
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            reporte.LocalReport.DataSources.Add(ds);

            reporte.LocalReport.ReportEmbeddedResource="SistemaDental.Report1.rdlc";
            reporte.RefreshReport();
        }
    }
}
