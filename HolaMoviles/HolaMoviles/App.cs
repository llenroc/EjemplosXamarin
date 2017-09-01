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
        public App()
        {
			MainPage = new MainPage();
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