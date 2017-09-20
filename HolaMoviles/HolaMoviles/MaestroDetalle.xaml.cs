using System;

namespace HolaMoviles
{
    public partial class MaestroDetalle
    {
        public MaestroDetalle()
        {
            InitializeComponent();

            botonNavegar.Clicked += BotonNavegar_Clicked;
        }

        private void BotonNavegar_Clicked(object sender, EventArgs e)
        {
            Detail = new Tabulacion();
        }
    }
}