using Xamarin.Forms;

namespace HolaMoviles
{

    public partial class DetallePersona : ContentPage
    {
        public DetallePersona()
        {
            InitializeComponent();
        }


        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                textoNombre.Text = value;
            }
        }
    }
}