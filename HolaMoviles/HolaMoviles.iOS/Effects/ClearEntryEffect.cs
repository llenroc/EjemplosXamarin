using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(HolaMoviles.iOS.Effects.ClearEntryEffect), "ClearEntryEffect")]
namespace HolaMoviles.iOS.Effects
{
	public class ClearEntryEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			ConfigureControl();
		}

		protected override void OnDetached()
		{
		}

		private void ConfigureControl()
		{
			((UITextField)Control).ClearButtonMode = UITextFieldViewMode.WhileEditing;
		}
	}
}