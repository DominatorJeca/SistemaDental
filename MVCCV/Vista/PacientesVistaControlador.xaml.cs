using System;
using System.Windows;
using System.Windows.Controls;

namespace SistemaDental.MVCCV.Vista
{
    /// <summary>
    /// Lógica de interacción para PacientesVistaControlador.xaml
    /// </summary>
    public partial class PacientesVistaControlador : UserControl
    {
        private bool Admin;
        private String Nombree;
        public PacientesVistaControlador()
        {
            InitializeComponent();
        }

        public PacientesVistaControlador(bool admin, string name)
        {
            InitializeComponent();
            Nombree = name;
            Admin = admin;
        }
        /*
                private void btnVerPaciente_Click(object sender, RoutedEventArgs e)
                {
                    VerPaciente verpaciente = new VerPaciente(Admin, Nombree);
                    verpaciente.Show();

                }
        */
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            AgregarPaciente agregarPaciente = new AgregarPaciente(Admin, Nombree);
            agregarPaciente.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*  Menu menu = new Menu(Admin, Nombree);
              menu.Show();*/

        }

        private void frmpaciente_Closed(object sender, EventArgs e)
        {
            /* Menu menu = new Menu(Admin, Nombree);
             menu.Show();*/


        }
    }
}
