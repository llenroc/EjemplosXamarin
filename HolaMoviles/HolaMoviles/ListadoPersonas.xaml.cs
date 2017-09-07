using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HolaMoviles.Modelos;
using Xamarin.Forms;

namespace HolaMoviles
{
	public partial class ListadoPersonas : ContentPage
	{
		public IList<Persona> Datos { get; set; }
		public ICommand ComandoRefrescar { get; set; }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        public ListadoPersonas()
		{
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
	}
}
