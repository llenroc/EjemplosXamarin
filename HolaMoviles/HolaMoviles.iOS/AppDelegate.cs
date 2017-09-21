using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using SQLite.Net;
using UIKit;

namespace HolaMoviles.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            var formsApp = new App();

            var fileName = "almacenamiento.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = System.IO.Path.Combine(documentsPath, "..", "Library");
            var path = System.IO.Path.Combine(libraryPath, fileName);

            formsApp.Contexto.RutaConexion = path;

            formsApp.Contexto.ObtenerConexion = () => ObtenerConexion(path);
            
            LoadApplication(formsApp);

            return base.FinishedLaunching(app, options);
        }

        private SQLiteConnection ObtenerConexion(string path)
        {
            return new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path);
        }
    }
}
