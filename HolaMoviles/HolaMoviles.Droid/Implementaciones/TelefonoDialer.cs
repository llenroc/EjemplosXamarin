using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HolaMoviles.Servicios;
using Android.Telephony;
using Xamarin.Forms;

namespace HolaMoviles.Droid
{
    public class TelefonoDialer : IDialer
    {
        public TelefonoDialer()
        {

        }
        public void Llamar(string numero)
        {
            var contexto = Forms.Context;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + numero));

            if (!PuedeLlamar(contexto, intent))
            {
                return;
            }
            contexto.StartActivity(intent);
        }

        private bool PuedeLlamar(Context contexto, Intent intent)
        {
            // https:codeshare.io/2EkAlo
            var packageManager = contexto.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                        .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            TelephonyManager mgr = TelephonyManager.FromContext(contexto);
            return mgr.PhoneType != PhoneType.None;
        }
    }
}