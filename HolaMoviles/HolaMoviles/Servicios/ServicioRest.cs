using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
					var llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

					if (llamada.IsSuccessStatusCode)
					{
						var resultado = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

						Debug.WriteLine(resultado);
					}
                    //var resultado = await httpClient.GetAsync("/").ConfigureAwait(false);

                    //var codigo = resultado.StatusCode;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
