using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HolaMoviles
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<Persona> ListaGeneral
		{
			get;
			set;
		}


		public MainPage()
		{
			InitializeComponent();
			ListaGeneral = new ObservableCollection<Persona>();
			listadoDatos.ItemsSource = ListaGeneral;

			boton.Clicked += (s, e) => {
				var mensaje = "Hola " + texto.Text;
				//await DisplayAlert("Mensaje", mensaje, "Cancelar");

				label.Text = mensaje;

				/*await Navigation.PushModalAsync(new DetallePersona() { 
					Nombre = texto.Text 
				});*/

				ListaGeneral.Add(new Contratista() { Nombre = texto.Text });
			};

            botonWeb.Clicked += (s, e) => {
                var servicio = new Servicios.ServicioRest();

                servicio.Conectar();
            };
        }
	}
}
