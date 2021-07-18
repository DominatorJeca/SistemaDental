using SistemaDental.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        }
    }
}
