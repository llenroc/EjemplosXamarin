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
                if (!CrossConnectivity.Current.IsConnected)
                {
                    return;
                }
                using (var httpClient = new HttpClient())
                {
					var llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

                    var paisPrueba = new Pais() { name = "Nueva entrada"};
                    string parseo = JsonConvert.SerializeObject(paisPrueba);

                    //await httpClient.PostAsync("/Inventarios", new StringContent(parseo, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                    Debug.WriteLine(parseo);
                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Pais[]>(json);

                        Debug.WriteLine(json);
                    }
                    //var resultado = await httpClient.GetAsync("/").ConfigureAwait(false);

                    //var codigo = resultado.StatusCode;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async void GetJason()
        {
            // https://restcountries.eu

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {
                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var resultado = JsonConvert.DeserializeObject<Pais[]>(json);

                        Debug.WriteLine(json);
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
