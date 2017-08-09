using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;

namespace HolaMoviles
{
	public partial class ImageCapturePage : ContentPage
	{
		protected byte[] MediaStream { get; set; }
		protected CognitiveService CognitiveService { get; set; } = new CognitiveService();
		protected ICollection<ImageCaptureViewModel> Captures { get; set; }

		private ImageCaptureViewModel _captureResult;
		public ImageCaptureViewModel CaptureResult
		{
			get { return _captureResult; }
			set
			{
				_captureResult = value;
				OnPropertyChanged(nameof(CaptureResult));
			}
		}

		public ImageCapturePage()
		{
			CaptureResult = new ImageCaptureViewModel();

			InitializeComponent();
			InitializeControls();
		}

		public ImageCapturePage(ICollection<ImageCaptureViewModel> captures) : this()
		{
			Captures = captures;
		}

		public ImageCapturePage(ICollection<ImageCaptureViewModel> allCaptures, ImageCaptureViewModel editCapture) : this(allCaptures)
		{
			CaptureResult = editCapture;

			MediaStream = null;
			LoadMedia();
		}

		private void InitializeControls()
		{
			BindingContext = this;

			buttonCapture.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, async () =>
				{
					await CaptureMedia();

				}).ConfigureAwait(false);       
			});


			buttonBrowse.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, async () =>
				{
					await BrowseMedia();

				}).ConfigureAwait(false);

			});

			buttonAnalyze.Command = new Command(async obj =>
			{
				await FormsUtils.RunAsBusyAsync(this, () => {

					AnalyzeMedia();
					AnalyzeText();
					Save();

				}).ConfigureAwait(false);
			});
		}

		private async void AnalyzeText()
		{
			if (MediaStream == null)
			{
				return;
			}
			var result = await CognitiveService.RecognizeTextAsync(MediaStream);

			StringBuilder builder = new StringBuilder();
			var allLines = result.Regions.SelectMany(region => region.Lines).ToList();

			foreach (var line in allLines)
			{
				var lineWords = line.Words.ToList();

				foreach (var word in lineWords)
				{
					builder.Append($"{word.Text}");

					if (lineWords.IndexOf(word) + 1 < lineWords.Count)
					{
						builder.Append(" ");
					}
				}

				if (allLines.IndexOf(line) + 1 < allLines.Count)
				{
					builder.Append(" ");
				}
			}
			CaptureResult.RecognizedText = builder.ToString();
		}

		private async Task CaptureMedia()
		{
			if (CrossMedia.Current.IsPickPhotoSupported)
			{
				var galeriaOpciones = new Plugin.Media.Abstractions.StoreCameraMediaOptions()
				{
					SaveToAlbum = false,
					CompressionQuality = 20
				};

				using (var media = await CrossMedia.Current.TakePhotoAsync(galeriaOpciones).ConfigureAwait(false))
				{
					MediaStream = await GetBytes(media.GetStream());
				}
			}
			LoadMedia();
		}

		private void LoadMedia()
		{
			if (MediaStream == null)
			{
				return;
			}
			CaptureResult.ImageSource = MediaStream;
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

			var result = await CognitiveService.AnalyzeImage(MediaStream).ConfigureAwait(false);

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

			CaptureResult.ImageSource = MediaStream;

			CaptureResult.PrimaryColor = FormsUtils.FromColor(result.Color.DominantColorBackground);
			CaptureResult.AccentColor = FormsUtils.FromColor(result.Color.AccentColor);
			CaptureResult.SecondaryColor = FormsUtils.FromColor(result.Color.DominantColorForeground);
		}

		private async void Save()
		{
			if (!Captures.Contains(CaptureResult))
			{
				Captures.Add(CaptureResult);
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