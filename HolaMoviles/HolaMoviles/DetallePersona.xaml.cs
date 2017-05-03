using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HolaMoviles
{

	public partial class DetallePersona : ContentPage
	{
		public DetallePersona()
		{
			InitializeComponent();

			botonUbicacion.Clicked += async (sender, e) =>
			{
				try
				{
					var posicion = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync().ConfigureAwait(false);

					Device.BeginInvokeOnMainThread(() => MostrarUbicacionActual(posicion));
				}
				catch (Exception ex)
				{
					DisplayAlert("Error", ex.Message, "Cerrar");
				}
			};
		}

		private void MostrarUbicacionActual(Plugin.Geolocator.Abstractions.Position posicion)
		{
			var pin = new Pin { Position = new Position(posicion.Latitude, posicion.Longitude) };
			pin.Label = "Ubicacion actual";

			var mapSpan = MapSpan.FromCenterAndRadius(
				new Position(posicion.Latitude, posicion.Longitude), Distance.FromMiles(1));

			try
			{
				Mapa.MoveToRegion(mapSpan);
				Mapa.Pins.Add(pin);
			}
			catch (System.Exception ex)
			{

			}
		}

		public DetallePersona(Persona persona)
		{
			InitializeComponent();

			Nombre = persona.Nombre;
		}

		private string _nombre;
		public string Nombre
		{
			get { return _nombre; }
			set
			{
				_nombre = value;
				textoNombre.Text = value;

			}
		}
	}
}