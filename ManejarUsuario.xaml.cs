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
    /// Interaction logic for ManejarUsuario.xaml
    /// </summary>
    public partial class ManejarUsuario : Window
    {
        private Usuario usuario = new Usuario();
        public ManejarUsuario()
        {
            InitializeComponent();
            MostrarUsuario();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }

        public void MostrarUsuario()
        {
            cmbUsuario.ItemsSource = usuario.MostrarUsuarios();
            cmbUsuario.SelectedValuePath = "Id";
            cmbUsuario.DisplayMemberPath = "Id";
        }

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
    }
}
