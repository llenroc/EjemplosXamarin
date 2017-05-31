using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolaMoviles
{
    public partial class MainPage : ContentPage
    {
		public ObservableCollection<ImageCaptureViewModel> MediaCaptures { get; set; }

        public MainPage()
        {
            InitializeComponent();
            
			MediaCaptures = new ObservableCollection<ImageCaptureViewModel>();
            
			BindingContext = this;

            buttonAdd.Clicked += async(s, e) =>
            {
				await Navigation.PushAsync(new ImageCapturePage(this));
			};
        }
    }
}