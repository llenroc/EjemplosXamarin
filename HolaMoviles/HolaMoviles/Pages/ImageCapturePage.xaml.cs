using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media;
using Xamarin.Forms;

namespace HolaMoviles
{
	public partial class ImageCapturePage : ContentPage
	{
		protected byte[] MediaStream { get; set; }
		protected CognitiveService CognitiveService { get; set; } = new CognitiveService();
		protected ImageCaptureViewModel CaptureResult;
		protected MainPage MainPage { get; private set; }

		public ImageCapturePage()
		{
			CaptureResult = new ImageCaptureViewModel();

			InitializeComponent();

			InitializeControls();
		}

		private void InitializeControls()
		{
			BindingContext = this;

			buttonCapture.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, async () =>
				{
					await CaptureMedia();
					AnalyzeMedia();

				}).ConfigureAwait(false);       
			});


			buttonBrowse.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, async () =>
				{
					await BrowseMedia();
					AnalyzeMedia();

				}).ConfigureAwait(false);

			});

			buttonAnalyze.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, AnalyzeMedia).ConfigureAwait(false);
			});

			buttonSave.Command = new Command(obj => Save());
		}

		private async Task CaptureMedia()
		{
			if (CrossMedia.Current.IsPickPhotoSupported)
			{
				var galeriaOpciones = new Plugin.Media.Abstractions.StoreCameraMediaOptions()
				{
					SaveToAlbum = false,
					CompressionQuality = 30
				};

				using (var media = await CrossMedia.Current.TakePhotoAsync(galeriaOpciones).ConfigureAwait(false))
				{
					MediaStream = await GetBytes(media.GetStream());
				}
			}
			LoadMedia();
		}

		void LoadMedia()
		{
			if (MediaStream == null)
			{
				return;
			}
			CaptureResult.ImageSource = ImageSource.FromStream(() => new MemoryStream(MediaStream));
			var image = new Image { Source = CaptureResult.ImageSource };

			Device.BeginInvokeOnMainThread(() => MediaContainer.Content = image);
		}

		public ImageCapturePage(MainPage mainPage)
			: this()
		{
			MainPage = mainPage;
		}

		private async Task BrowseMedia()
		{
			if (CrossMedia.Current.IsPickPhotoSupported)
			{
				var galeriaOpciones = new Plugin.Media.Abstractions.PickMediaOptions()
				{
					CompressionQuality = 30
				};

				using (var media = await CrossMedia.Current.PickPhotoAsync(galeriaOpciones).ConfigureAwait(false))
				{
					MediaStream = await GetBytes(media.GetStream());
				}
			}
			LoadMedia();
		}

		private async void AnalyzeMedia()
		{
			if (MediaStream == null)
			{
				return;
			}
			CaptureResult.Description = "Analyzing...";

			var result = await CognitiveService.AnalyzeImage(new MemoryStream(MediaStream)).ConfigureAwait(false);

			if (result == null)
			{
				return;
			}
			var caption = (from match in result.Description.Captions
			               where match.Confidence.Equals(result.Description.Captions.Max(item => item.Confidence))
							select match).FirstOrDefault();

			CaptureResult.Confidence = caption.Confidence * 100;
			CaptureResult.Description = caption.Text;
			CaptureResult.VerbosedDescription = $" {caption.Text}({(caption.Confidence * 100).ToString("F2")}%)";

			CaptureResult.ImageSource = ImageSource.FromStream(() => new MemoryStream(MediaStream));

			CaptureResult.PrimaryColor = FormsUtils.FromColor(result.Color.DominantColorBackground);
			CaptureResult.AccentColor = FormsUtils.FromColor(result.Color.AccentColor);
			CaptureResult.SecondaryColor = FormsUtils.FromColor(result.Color.DominantColorForeground);
		}

		private async void Save()
		{
			try
			{
				MainPage?.MediaCaptures.Add(CaptureResult);
			}
			catch (Exception ex)
			{

			}
			await Navigation.PopAsync();
		}

		private async Task<byte[]> GetBytes(Stream stream)
		{
			var bytes = new byte[stream.Length];
			await stream.ReadAsync(bytes, 0, (int)stream.Length);

			return bytes;
		}
	}
}