using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolaMoviles
{
    public partial class MaestroDetalle
    {
        public MaestroDetalle()
        {
            InitializeComponent();

            Master = new ContentPage() { Title = "Master" };

            botonNavegar.Clicked += BotonNavegar_Clicked;
        }

        private void BotonNavegar_Clicked(object sender, EventArgs e)
        {
            Detail = new Tabulacion();
        }
    }
}