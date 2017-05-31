using System.IO;
using Xamarin.Forms;

namespace HolaMoviles
{
	public class ImageCaptureViewModel : BindableObject
	{
		private string _description;

		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		private string _verbosedDescription;
		public string VerbosedDescription
		{
			get { return _verbosedDescription; }
			set
			{
				_verbosedDescription = value;
				OnPropertyChanged(nameof(VerbosedDescription));
			}
		}

		private Color _accentColor;
		public Color AccentColor
		{
			get { return _accentColor; }
			set
			{
				_accentColor = value;
				OnPropertyChanged(nameof(AccentColor));
			}
		}

		private Color _primaryColor;
		public Color PrimaryColor
		{
			get { return _primaryColor; }
			set
			{
				_primaryColor = value;
				OnPropertyChanged(nameof(PrimaryColor));
			}
		}

		private Color _secondaryColor;
		public Color SecondaryColor
		{
			get { return _secondaryColor; }
			set
			{
				_secondaryColor = value;
				OnPropertyChanged(nameof(SecondaryColor));
			}
		}

		private ImageSource _imageSource;
		public ImageSource ImageSource
		{
			get { return _imageSource; }
			set
			{
				_imageSource = value;
				OnPropertyChanged(nameof(ImageSource));
			}
		}

		private double _confidence;
		public double Confidence
		{
			get { return _confidence; }
			set
			{
				_confidence = value;
				OnPropertyChanged(nameof(Confidence));
			}
		}
	}
}