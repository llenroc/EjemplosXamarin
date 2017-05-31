using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HolaMoviles
{
	public partial class App
	{
		public App()
		{
			CrossMedia.Current.Initialize();

			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}
	}
}
