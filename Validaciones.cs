using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace SistemaDental
{
    class Validaciones
    {

        public Validaciones() { }

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

        
        public bool VerificarCampos(Window window)
        {
            bool band = true;
            foreach (var tb in FindVisualChildren<TextBox>(window))
            {
                if (tb.Text.Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox")
                    band = false;
            }

            foreach(var tb in FindVisualChildren<ComboBox>(window))
            {
                if (tb.SelectedValue.ToString().Replace(" ", "").Equals("") && tb.Name != "PART_EditableTextBox" && tb.Name != "PART_TextBox")
                    band = false;
            }

            foreach (var tb in FindVisualChildren<DataGrid>(window))
            {
                if (tb.SelectedValue==null)
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
       
       
    }
}
