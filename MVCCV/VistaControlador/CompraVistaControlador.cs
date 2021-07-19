using SistemaDental.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDental.MVCCV.VistaControlador
{
    class CompraVistaControlador : ObservableObject
    {

        ClaseInventario invent = new ClaseInventario();
        ClaseProcedimiento proc = new ClaseProcedimiento();
        private ObservableCollection<ClaseInventario> DatosDataGrid = new ObservableCollection<ClaseInventario>();
        private ObservableCollection<ClaseInventario> DatosCompra = new ObservableCollection<ClaseInventario>();
        public RelayCommand LoadComand { get; set; }
        public RelayCommand EjecutarCompra { get; set; }
        public ObservableCollection<ClaseInventario> datosCompra { get { return DatosCompra; } set { DatosCompra = value; OnPropertyChanged(); } }
        public ObservableCollection<ClaseInventario> datosDatagrid { get { return DatosDataGrid; } set { DatosDataGrid = value; OnPropertyChanged(); } }

        public  CompraVistaControlador()
            {
            datosDatagrid = new ObservableCollection<ClaseInventario>( proc.MostrarInventario());
            LoadComand = new RelayCommand(o => { datosDatagrid = new ObservableCollection<ClaseInventario>(proc.MostrarInventario()); });
            EjecutarCompra = 
                new RelayCommand(o =>
           { 
           
           
           
           });
            }


    }
}
