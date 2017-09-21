using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HolaMoviles.Modelos;
using Xamarin.Forms;
using HolaMoviles.Servicios;
using Plugin.Connectivity;
using HolaMoviles.Data;
using System.Linq;

namespace HolaMoviles
{
	public partial class ListadoPersonas : ContentPage
	{
		public IList<Persona> Datos { get; set; }
		public ICommand ComandoRefrescar { get; set; }

        public ContextoDatos Contexto { get; set; }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        public ListadoPersonas()
		{
            Contexto = DependencyService.Get<ContextoDatos>();

            IsBusy = false;

			Datos = new ObservableCollection<Persona> { 
				new Persona { Nombre = "Esteban" },
				new Persona { Nombre = "Oscar" } 
			};

			ComandoRefrescar = new Command((obj) => {

				Datos.Add(new Persona { Nombre = "Otro mas" });
				IsBusy = false; 
			});

			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            IsBusy = true;

            var servicio = new ServicioPersonas();

            Persona[] resultado;

            if (CrossConnectivity.Current.IsConnected)
            {
                resultado = await servicio.ObtenerConCodigo();
                Contexto.Guardar(resultado);
            }
            else
            {
                resultado = Contexto.Obtener().ToArray();
            }

            Datos.Clear();

            foreach (var item in resultado)
            {
                Datos.Add(item);
            }

            IsBusy = false;
        }
    }
}
