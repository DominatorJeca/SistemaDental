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
        public VerPacienVistaControlador verPacienVista { get; set; }
        public TratamientosvistaControlador tratamientosvista { get; set; }
        public EdditarUsuariovistaControlador edditarUsuariovista { get; set; }
        public InventarioVistaControlador  inventarioVista { get; set; }
        public ManejarusuariovistaControlador manejarusuariovista { get; set; }

        private object _vistaActual;
        public RelayCommand CasaVistaComando { get; set; }
        public RelayCommand CajaVistaComando { get; set; }
        public RelayCommand verPacienVistaComando { get; set; }
        public RelayCommand tratamientoVistaComando { get; set; }
        public RelayCommand EdditarUsuariovistaComando { get; set; }
        public RelayCommand InventarioVistaComando { get; set; }
        public RelayCommand manejarusuariovistaComando { get; set; }

        public CitaVistaControlador CitaVista { get; set; }
        public AjusteVistaControlador AjusteVista { get; set; }

        public RelayCommand CitaVistaComando { get; set; }
        public RelayCommand AjusteVistaComando { get; set; }
        public CompraVistaControlador CompraVista { get; set; }
        public RelayCommand CompraVistaComando { get; set; }
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
            verPacienVista = new VerPacienVistaControlador ();
            tratamientosvista = new TratamientosvistaControlador();
            edditarUsuariovista = new EdditarUsuariovistaControlador();
            inventarioVista = new InventarioVistaControlador();
            manejarusuariovista = new ManejarusuariovistaControlador();

            CitaVista = new CitaVistaControlador();
            AjusteVista = new AjusteVistaControlador();
            CompraVista = new CompraVistaControlador();
            vistaActual = CasaVista;
            CasaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CasaVista;
            });
            CajaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CajaVista;

            });
            verPacienVistaComando = new RelayCommand(o =>
            {
                vistaActual = verPacienVista;
            });
            tratamientoVistaComando = new RelayCommand(o =>
            {
                vistaActual = tratamientosvista;
            });

            EdditarUsuariovistaComando = new RelayCommand(o =>
            {
                vistaActual = edditarUsuariovista;
            });
            InventarioVistaComando = new RelayCommand(o =>
            {
                vistaActual = edditarUsuariovista;
            });
            manejarusuariovistaComando = new RelayCommand(o =>
            {
                vistaActual = manejarusuariovista;
            });


            CitaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CitaVista;


            });
            AjusteVistaComando = new RelayCommand(o =>
            {
                vistaActual = AjusteVista;
            });

            CompraVistaComando = new RelayCommand(o =>
            {
                vistaActual = CompraVista;
            });
        }


    }
}
