using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Input;





namespace SistemaDental
{
    public class Validaciones
    {

        public Validaciones() { }

        public void SoloLetras(TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }

        public void SoloNumeros(TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        public bool VerificarCampos(UserControl window)
        {
            bool band = true;
            foreach (var tb in FindVisualChildren<TextBox>(window))
            {
                if (tb.Text.Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox")
                    band = false;
            }

            foreach(var tb in FindVisualChildren<ComboBox>(window))
            {
                if (tb.SelectedValue == null)
                    band = false;
            }


            foreach (var tb in FindVisualChildren<RadioButton>(window))
            {
                if (tb.IsChecked == false)
                    band = false;
            }
            return band;
        }
        public bool verificarpass(UserControl window)
        {
            bool band = true;
            foreach (var tb in FindVisualChildren<PasswordBox>(window))
            {
                if (tb.Password.Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox")
                    band = false;
            }
            return band;
        }
        public bool Verificardgv(UserControl window)
        {
            bool band = true;

            foreach (var tb in FindVisualChildren<DataGrid>(window))
            {
                if (tb.SelectedValue == null)
                    band = false;
            }
            return band;
        }

        public bool VerificarCantidad(double cantidad)
        {
            bool var = true;

            if (cantidad <= 0)
            {
                var = false;

            }
            return var;
        }

        public bool VerificarIdentidad(string identidad)
        {
            bool var = true;

            if (identidad.Length != 13)
                var = false;

            return var;

        }

        public bool VerificarNumero(string numero)
        {
            bool siono = false;
            if(numero.Length == 8)
            {
                siono = true;
            }

            return siono;
        }

        public bool VerificarFecha(DateTime Fecha)
        {
            DateTime presente = DateTime.Today;
            bool siono = false;
            int anio = Fecha.Year;
            if (anio > 1900 && anio < presente.Year - 1)
            {
                siono = true;
            }

            return siono;
        }

    }
}
