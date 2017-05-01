using System;
using HolaMoviles.Servicios;
using UIKit;

namespace HolaMoviles.iOS
{
	public class TelefonoDialer : IDialer
	{
		public void Llamar(string numero)
		{
			UIApplication.SharedApplication.OpenUrl(
				new Foundation.NSUrl("tel:" + numero)
			);
		}
	}
}