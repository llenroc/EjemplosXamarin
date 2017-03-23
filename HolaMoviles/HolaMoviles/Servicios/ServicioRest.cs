using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HolaMoviles.Servicios
{
    public class ServicioRest
    {

        public async void Conectar()
        {
            // https://restcountries.eu

            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://restcountries.eu/rest/v2");
                    var resultado = await httpClient.GetStringAsync("/all").ConfigureAwait(false);

                    //var resultado = await httpClient.GetAsync("/all").ConfigureAwait(false);

                    //var codigo = resultado.StatusCode;
                    Debug.WriteLine(resultado);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
