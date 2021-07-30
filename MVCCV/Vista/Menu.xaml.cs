﻿using SistemaDental.MVCCV.Vista;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
       //Variables miembros
        private bool Admin;
        private String Nombree;
      
        private ClaseProcedimiento proced = new ClaseProcedimiento();

        public Turno turno = new Turno();
        private CajaVista VistaCaja = new CajaVista();
        private MenuInicioVista VistaMenuInicio = new MenuInicioVista();
        private DatosDeUsuario VistaUsuarioIngresado = new DatosDeUsuario();
        private AjustesVista VistaAjuste = new AjustesVista();
        //Constructores
        public Menu()
        {
            InitializeComponent();
            ContenedorHijos.Content = VistaMenuInicio;
            VistaMenuInicio.CambioDeVistaPrincipal += CambiarVista;
            VistaAjuste.CambioDeVistaPrincipal += CambiarVista;
        }

        public Menu(bool admin,string name,int id)
        {

            InitializeComponent();
            //lblUsuario.Content = name;
            PermisosAdministrador(admin);
            Nombree = name;
            Admin = admin;
            turno.UsuarioID = id;
            turno.ComienzoTurno = DateTime.Now;
            proced.AgregarTurno(turno);
            ContenedorHijos.Content = VistaMenuInicio;
        }
        private void CambiarVista(object o,EventArgs e)
        {
            ContenedorHijos.Content = o;
        }

        /// <summary>
        /// Verifica si el usuario que ingreso tiene permisos de administrador
        /// </summary>
        /// <param name="admin"></param>
        public void PermisosAdministrador(bool admin)
        {
            if (admin)
            {
                btnAjustes.IsEnabled = true;
            }
            else
            {
                btnAjustes.IsEnabled = false;
            }

        }

        /// <summary>
        /// Abre el Formulario de inventario y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario inventario = new Inventario(Admin,Nombree);
            inventario.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el Formulario de tratamiento y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTratamiento_Click(object sender, RoutedEventArgs e)
        {
            Tratamiento tratamiento = new Tratamiento(Admin, Nombree);
            tratamiento.Show();
            this.Hide();
        }
        /// <summary>
        /// Abre el Formulario de caja y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaja_Click(object sender, RoutedEventArgs e)
        {
          
          
        }
        /// <summary>
        /// Abre el Formulario de pacientes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
          
            this.Hide();
        }

        /// <summary>
        /// Abre el Formulario de ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Abre el Formulario de Citas y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
           
           
            this.Hide();
        }

        private void btnRealizar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {


            MessageBoxResult result = MessageBox.Show("¿Deseas terminar tu turno?", "Turno", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        turno.FinalTurno = DateTime.Now;
                        proced.ActualizarTurno(turno);
                        Login log = new Login();
                        log.Show();
                        this.Close();
                        break;
                    }
                case MessageBoxResult.No:
                    {
                        Login log = new Login();
                            log.Show();
                        this.Close();
                        break;
                    }
                case MessageBoxResult.Cancel:
                    break;
            }



        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista(VistaMenuInicio,null);
        }

        private void btnCaja_Click_1(object sender, RoutedEventArgs e)
        {
            CambiarVista(VistaCaja, null);
        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista(VistaUsuarioIngresado, null);
        }

        private void btnAjustes_Click_1(object sender, RoutedEventArgs e)
        {
            CambiarVista(VistaAjuste, null);
        }
    }
}
