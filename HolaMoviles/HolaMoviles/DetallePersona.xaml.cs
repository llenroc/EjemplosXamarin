using System;
using Plugin.Media;
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
					await DisplayAlert("Error", ex.Message, "Cerrar");
				}
			};

			botonFoto.Clicked += async(sender, e) => 
			{
				try
				{
					Plugin.Media.Abstractions.MediaFile foto = null;

					if (CrossMedia.Current.IsCameraAvailable)
					{
						var fotoOpciones = new Plugin.Media.Abstractions.StoreCameraMediaOptions()
						{
							Directory = "Xamarin",
							Name = $"{DateTime.UtcNow}.jpg"
						};

						foto = await CrossMedia.Current.TakePhotoAsync(fotoOpciones);
					}

					if (foto == null && CrossMedia.Current.IsPickPhotoSupported)
					{

						var galeriaOpciones = new Plugin.Media.Abstractions.PickMediaOptions()
						{
							CompressionQuality = 100
						};

						foto = await CrossMedia.Current.PickPhotoAsync(galeriaOpciones);
					}

					if (foto == null)
					{
						return;
					}
					var imagen = new Image { Source = ImageSource.FromFile(foto.Path) };

					await Navigation.PushAsync(new ContentPage { Title = "Imagen cargada", Content = imagen });
				}
				catch (Exception ex)
				{
					await DisplayAlert("Error", ex.Message, "Cerrar");
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