﻿using SistemaDental.Base;
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
        private object _vistaActual;
        public RelayCommand CasaVistaComando { get; set; }
        public RelayCommand CajaVistaComando { get; set; }
        public RelayCommand verPacienVistacomando { get; set; }
        public RelayCommand  tratamientoVistaCommand { get; set; }
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

            vistaActual = CasaVista;
            CasaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CasaVista;
            });
            CajaVistaComando = new RelayCommand(o =>
            {
                vistaActual = CajaVista;
            });
            verPacienVistacomando = new RelayCommand(o =>
            {
                vistaActual = verPacienVista;
            });
            tratamientoVistaCommand = new RelayCommand(o =>
            {
                vistaActual = tratamientosvista;
            });



        }
    }
}
