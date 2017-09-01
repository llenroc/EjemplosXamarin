using System;
using System.Collections.ObjectModel;
using HolaMoviles.Modelos;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace HolaMoviles
{
	public partial class MainPage : ContentPage
	{
		public Persona MiObjeto { get; } = null;

		public MainPage()
		{
			InitializeComponent();

			botonDatos.Clicked += (sender, e) => {
				Navigation.PushModalAsync(new ListadoPersonas());
			};

			MiObjeto = new Persona();

			Action hacerAlgo = new Action(HandleAction);

			//boton1.Command = new Command((obj) => hacerAlgo());

			boton1.Clicked += (sender, e) =>
			{
				MiObjeto.Nombre = $"hola:{ MiObjeto.Nombre}";

				hacerAlgo();



				//hacerAlgo();

				//hacerAlgo();
			};
		}

		async void HandleAction()
		{
			await Task.Delay(2000);

			textoNombre.Text = "cambio en C#";

			var elementos = new[] { 3, 5, 7, 9 };

			await DisplayAlert("Mensaje importante", "Promedio:" + elementos.Average(), "Cancelar");
		}
	}
}