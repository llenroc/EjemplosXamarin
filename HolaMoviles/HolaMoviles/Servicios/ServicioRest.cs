using HolaMoviles.Modelos;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
                if (CrossConnectivity.Current.IsConnected == false)
                {
                    return;
                }
                using (var httpClient = new HttpClient())
                {

					HttpResponseMessage llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

                    // httpClient.BaseAddress = new Uri("www.miservidor.com/rest/v1/");
                    
                    var paisPrueba = new Pais() { Nombre = "Nueva entrada" };
                    string parseo = JsonConvert.SerializeObject(paisPrueba);

                    //await httpClient.PostAsync("/Inventarios", new StringContent(parseo, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                    Debug.WriteLine(parseo);
                    
					if (llamada.IsSuccessStatusCode)
					{
						var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Pais[]>(json);
                        
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

        public async void GetJson()
        {
            // https://restcountries.eu

            try
            {

                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

                    // httpClient.BaseAddress = new Uri("www.miservidor.com/rest/v1/");
                    // httpClient.PostAsync("/Inventarios", new HttpContent());

                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Pais[]>(json);

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
