using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HolaMoviles.Droid
{
    [Activity(Label = "HolaMoviles", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var formsApp = new App();
            formsApp.Contexto.RutaConexion = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            formsApp.Contexto.RutaConexion += "/almacenamiento.db3";
            formsApp.Contexto.ObtenerConexion = () => new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), formsApp.Contexto.RutaConexion);
            LoadApplication(formsApp);
        }
    }
}

