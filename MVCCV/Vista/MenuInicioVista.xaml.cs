using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para MenuInicioVista.xaml
    /// </summary>
    public partial class MenuInicioVista : UserControl
    {
        public event EventHandler CambioDeVistaPrincipal;
       public CitaVista VistaAgendarCita = new CitaVista();
        private CompraVista VistaCompras = new CompraVista();
        private InventarioVista VistaInventario = new InventarioVista();
        private TratamientoVista VistaTratamiento = new TratamientoVista();
        private VerPacienVista VistaPacientes = new VerPacienVista();
        private MenuReporteVista VistaMenuReportes = new MenuReporteVista();
        public Usuario user = new Usuario();
        public MenuInicioVista()
        {
            InitializeComponent();
            
        }

        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost/ProjectoClinicaDental/vistas/";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        private void btnTratamientos_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void CambioDeVista(object o)
        {
               if (CambioDeVistaPrincipal != null)
                    CambioDeVistaPrincipal(o, null);
        }
        private void btnTratamientos_Click_1(object sender, RoutedEventArgs e)
        {
            VistaTratamiento.user = user;
            CambioDeVista(VistaTratamiento);
        }

        private void btnCitas_Click_1(object sender, RoutedEventArgs e)
        {
            VistaAgendarCita.CambioDeVistaPrincipal += CambioDeVistaPrincipal;
            VistaAgendarCita.user = user;
            CambioDeVista(VistaAgendarCita);
        }

        private void btnCompra_Click(object sender, RoutedEventArgs e)
        {
            VistaCompras.user = user;
            CambioDeVista(VistaCompras);
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            CambioDeVista(VistaPacientes);
        }

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            CambioDeVista(VistaInventario);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            
            CambioDeVista(VistaMenuReportes);
            
        }
    }
}
