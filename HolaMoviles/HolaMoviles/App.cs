using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HolaMoviles
{
    public class App : Application
    {
        // stvansolano@outlook.com
        public App()
        {
			MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
    }
}
