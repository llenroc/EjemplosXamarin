using HolaMoviles.Servicios;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HolaMoviles
{
	public partial class DetallePersona : ContentPage
	{
		public DetallePersona(Persona persona)
		{
			InitializeComponent();

            Nombre = persona.Nombre;

            botonMarcado.Command = new Command(() => {
                if (string.IsNullOrEmpty(textoTelefono.Text))
                {
                    return;
                }

                var marcado = DependencyService.Get<IDialer>();

                marcado.Llamar(textoTelefono.Text);
            });

        }

		private string _nombre;
		public string Nombre { 
			get { return _nombre; } 
			set
			{
				_nombre = value;
				textoNombre.Text = value;
			}
		}
	}
}