using Microsoft.Xaml.Behaviors.Core;
using SistemaDental.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaDental.MVCCV.VistaControlador
{
    class MenuPrincipalVistaControlador: ObservableObject
    {
        public CasaVistaControlador CasaVista { get; set; }
        public CajaVistaControlador CajaVista { get; set; }
        public CitaVistaControlador CitaVista { get; set; }
        public AjusteVistaControlador AjusteVista { get; set; }
        private object _vistaActual;
        public RelayCommand CasaVistaComando { get; set; }
        public RelayCommand CajaVistaComando { get; set; }
        public RelayCommand CitaVistaComando { get; set; }
        public RelayCommand AjusteVistaComando { get; set; }
        public object vistaActual
        {
            get { return _vistaActual; }
            set { _vistaActual = value;
                OnPropertyChanged();
            }
        }
        public MenuPrincipalVistaControlador()
        {
            CasaVista = new CasaVistaControlador();
            CajaVista = new CajaVistaControlador();
            CitaVista = new CitaVistaControlador();
            AjusteVista = new AjusteVistaControlador();
            vistaActual = CasaVista;
            CasaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CasaVista;
            });
            CajaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CajaVista;
       
            });
            CitaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CitaVista;
            

            });
            AjusteVistaComando = new RelayCommand(o =>
            {
                vistaActual = AjusteVista;
            });

        }
     
        
    }
}
