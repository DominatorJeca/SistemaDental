using System;
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
            VistaManejarUsuario.user = user;
        }

        public AjustesVista(bool admin, string name)
        {
            InitializeComponent();
            Nombree = name;
            Admin = admin;
            VistaManejarUsuario.user = user;
        }
        protected virtual void CambioDeVista(object o)
        {
            if (CambioDeVistaPrincipal != null)
                CambioDeVistaPrincipal(o, null);
        }

        private void btnManejarUsuarios_Click(object sender, RoutedEventArgs e)
        {
           
            CambioDeVista(VistaManejarUsuario);
        }
    }
}
