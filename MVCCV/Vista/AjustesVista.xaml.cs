using System;
using System.Windows;
using System.Windows.Controls;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para AjustesVista.xaml
    /// </summary>
    public partial class AjustesVista : UserControl
    {
        public Usuario user = new Usuario();
        private bool Admin;
        private String Nombree;
        private VistaAgregarUsuario VistaManejarUsuario = new VistaAgregarUsuario();
        public event EventHandler CambioDeVistaPrincipal;

        public AjustesVista()
        {
            InitializeComponent();

        }

        public AjustesVista(bool admin, string name)
        {
            InitializeComponent();
            Nombree = name;
            Admin = admin;

        }
        protected virtual void CambioDeVista(object o)
        {
            if (CambioDeVistaPrincipal != null)
            {
                CambioDeVistaPrincipal(o, null);
            }
        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            VistaManejarUsuario.user = user;
            CambioDeVista(VistaManejarUsuario);
        }
    }
}
