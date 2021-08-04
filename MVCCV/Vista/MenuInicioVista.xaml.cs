using System;
using System.Windows;
using System.Windows.Controls;

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
            string url = "http://sistemadentalhn.epizy.com/ProjectoClinicaDental/vistas/index.php";
            System.Diagnostics.Process.Start(url);
        }

        private void btnTratamientos_Click(object sender, RoutedEventArgs e)
        {

        }

        protected virtual void CambioDeVista(object o)
        {
            if (CambioDeVistaPrincipal != null)
            {
                CambioDeVistaPrincipal(o, null);
            }
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
            VistaPacientes.usuar = user;
            CambioDeVista(VistaPacientes);
        }

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            CambioDeVista(VistaInventario);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            VistaMenuReportes.user = user;
            CambioDeVista(VistaMenuReportes);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            permisosPuesto();
        }
        private void permisosPuesto()
        {
            if (user.PuestoNombre == "Asistente" && !user.Administrador)
            {
                btnTratamientos.IsEnabled = false;
                btnInventario.IsEnabled = false;
                btnCompra.IsEnabled = false;
            }
            if (user.PuestoNombre == "Secretario" && !user.Administrador)
            {
                btnTratamientos.IsEnabled = false;
            }
        }
    }
}
