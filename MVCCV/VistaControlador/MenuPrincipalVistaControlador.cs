using SistemaDental.Base;

namespace SistemaDental.MVCCV.VistaControlador
{
    internal class MenuPrincipalVistaControlador : ObservableObject
    {
        public DatosDeUsuarioVistaControlador DatosDeUsuarioVista { get; set; }
        public CasaVistaControlador CasaVista { get; set; }
        public CajaVistaControlador CajaVista { get; set; }
        public UsuariosVistaControlador UsuarioVista { get; set; }
        public Pacientesvistacontrolador PacientesVista { get; set; }
        public TratamientosvistaControlador tratamientosvista { get; set; }
        public EdditarUsuariovistaControlador edditarUsuariovista { get; set; }
        public InventarioVistaControlador inventarioVista { get; set; }
        public ManejarusuariovistaControlador manejarusuariovista { get; set; }

        public CitaVistaControlador CitaVista { get; set; }
        public AjusteVistaControlador AjusteVista { get; set; }
        public CompraVistaControlador CompraVista { get; set; }
        public MenuReporteVistaControlador menuReporteVista { get; set; }

        private object _vistaActual;
        public RelayCommand CasaVistaComando { get; set; }
        public RelayCommand UsuarioVistaComando { get; set; }

        public RelayCommand CajaVistaComando { get; set; }
        public RelayCommand verPacienVistaComando { get; set; }
        public RelayCommand tratamientoVistaComando { get; set; }
        public RelayCommand EdditarUsuariovistaComando { get; set; }
        public RelayCommand InventarioVistaComando { get; set; }
        public RelayCommand manejarusuariovistaComando { get; set; }

        public RelayCommand MenuReporteVistaComando { get; set; }
        public RelayCommand CitaVistaComando { get; set; }
        public RelayCommand AjusteVistaComando { get; set; }

        public RelayCommand CompraVistaComando { get; set; }

        public RelayCommand DatosDeUsuarioVistaComando { get; set; }
        public object vistaActual
        {
            get { return _vistaActual; }
            set
            {
                _vistaActual = value;
                OnPropertyChanged();
            }
        }
        public MenuPrincipalVistaControlador()
        {
            CasaVista = new CasaVistaControlador();
            CajaVista = new CajaVistaControlador();
            UsuarioVista = new UsuariosVistaControlador();
            PacientesVista = new Pacientesvistacontrolador();
            tratamientosvista = new TratamientosvistaControlador();
            edditarUsuariovista = new EdditarUsuariovistaControlador();
            inventarioVista = new InventarioVistaControlador();
            manejarusuariovista = new ManejarusuariovistaControlador();
            menuReporteVista = new MenuReporteVistaControlador();
            CitaVista = new CitaVistaControlador();
            AjusteVista = new AjusteVistaControlador();
            CompraVista = new CompraVistaControlador();
            DatosDeUsuarioVista = new DatosDeUsuarioVistaControlador();
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
                vistaActual = PacientesVista;
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
                vistaActual = inventarioVista;
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
            MenuReporteVistaComando = new RelayCommand(o =>
            {
                vistaActual = menuReporteVista;
            });

            UsuarioVistaComando = new RelayCommand(o =>
           {
               vistaActual = UsuarioVista;
           });
            DatosDeUsuarioVistaComando = new RelayCommand(o =>
             {
                 vistaActual = DatosDeUsuarioVista;
             });
        }


    }
}
