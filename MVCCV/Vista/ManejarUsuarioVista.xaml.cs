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
    /// Lógica de interacción para ManejarUsuarioVista.xaml
    /// </summary>
    public partial class ManejarUsuarioVista : UserControl
    {
        //variables miembro
        private Usuario usuario = new Usuario();
        private bool Admin;
        private String Nombree;

        //Contructores
        public ManejarUsuario()
        {
            InitializeComponent();
            MostrarUsuario();

        }

        public ManejarUsuario(bool admin, string name)
        {
            InitializeComponent();
            MostrarUsuario();

            Nombree = name;
            Admin = admin;
        }

        /// <summary>
        /// Abre el formulario de ajustes y cierra el actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        /// <summary>
        /// Asigna la lista de usuarios al combobox
        /// </summary>
        public void MostrarUsuario()
        {
            cmbUsuario.ItemsSource = usuario.MostrarUsuarios();
            cmbUsuario.SelectedValuePath = "Id";
            cmbUsuario.DisplayMemberPath = "Id";
        }

        public void MostrarUsuarioDesactivo()
        {
            cmbUsuario.ItemsSource = usuario.MostrarUsuariosDesactivos();
            cmbUsuario.SelectedValuePath = "Id";
            cmbUsuario.DisplayMemberPath = "Id";
        }


        /// <summary>
        /// Llama el metodo de eliminar usuario de la clase Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbUsuario.SelectedValue == null)
                    MessageBox.Show("Por favor selecciona un empleado");
                else
                {
                    // Mostrar un mensaje de confirmación
                    MessageBoxResult result = MessageBox.Show("¿Deseas eliminar el empleado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Eliminar el empleado
                        usuario.EliminarUsuario(Convert.ToString(cmbUsuario.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de eliminar el empleado...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el combobox de empleados
                MostrarUsuario();
            }
        }
        /// <summary>
        /// Llama el metodo de dar privilegios de la clase usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrivilegios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario elUsuario = usuario.BuscarUsuario(Convert.ToString(cmbUsuario.SelectedValue));


                //Verificar si el usuario existe

                if (cmbUsuario.SelectedValue == null)
                {
                    MessageBox.Show("Por favor selecciona un empleado");
                }
                else if (elUsuario.Administrador == true)
                {
                    MessageBox.Show("El usuario ya tiene permisos de administrador");
                }
                else
                {
                    // Mostrar un mensaje de confirmación
                    MessageBoxResult result = MessageBox.Show("¿Deseas dar privilegios de administrador al empleado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Eliminar el empleado
                        usuario.PrivilegioUsuario(Convert.ToString(cmbUsuario.SelectedValue));
                        MessageBox.Show("Permisos asignados correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de dar privilegio al empleado...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el combobox de empleados
                MostrarUsuario();
            }
        }

        private void btnRestaurarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbUsuario.SelectedValue == null)
                    MessageBox.Show("Por favor selecciona un empleado");
                else
                {
                    // Mostrar un mensaje de confirmación
                    MessageBoxResult result = MessageBox.Show("¿Deseas restaurar el empleado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Eliminar el empleado
                        usuario.RestaurarUsuario(Convert.ToString(cmbUsuario.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de restaurar el empleado...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el combobox de empleados
                MostrarUsuario();
                CKActivo.IsChecked = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (CKActivo.IsChecked == true)
            {
                MostrarUsuario();
                btnEliminarUsuario.IsEnabled = true;
                btnPrivilegios.IsEnabled = true;
                btnRestaurarUsuario.IsEnabled = false;
            }


        }



        private void CKActivo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CKActivo.IsChecked == false)
            {
                MostrarUsuarioDesactivo();
                btnEliminarUsuario.IsEnabled = false;
                btnPrivilegios.IsEnabled = false;
                btnRestaurarUsuario.IsEnabled = true;
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Ajustes ajustes = new Ajustes(Admin, Nombree);
            ajustes.Show();
        }
    }
}
