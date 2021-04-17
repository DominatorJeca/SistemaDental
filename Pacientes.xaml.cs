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
using System.Windows.Shapes;

namespace SistemaDental
{
    /// <summary>
    /// Interaction logic for Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window
    {
        private bool Admin;
        private String Nombree;
        public Pacientes()
        {
            InitializeComponent();
        }

        public Pacientes(bool admin, string name)
        {
            InitializeComponent();
            Nombree = name;
            Admin = admin;
        }

        private void btnVerPaciente_Click(object sender, RoutedEventArgs e)
        {
            VerPaciente verpaciente = new VerPaciente(Admin, Nombree);
            verpaciente.Show();
            this.Hide();
        }

        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            AgregarPaciente agregarPaciente = new AgregarPaciente(Admin, Nombree);
            agregarPaciente.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(Admin,Nombree);
            menu.Show();
            this.Hide();
        }
    }
}
