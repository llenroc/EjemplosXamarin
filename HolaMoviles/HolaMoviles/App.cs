using HolaMoviles.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HolaMoviles
{
    public class App : Application
    {
        public ContextoDatos Contexto { get; set; }

        public App()
        {
            Contexto = new ContextoDatos();
			MainPage = new NavigationPage(new MaestroDetalle()) { Title = "Ejemplos Xamarin" };
        }

		protected override void OnStart()
		{
			base.OnStart();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}
    }
}

// Descargar y abrir en VS2017:
//https://codeshare.io/2WeQnY