using HolaMoviles.Modelos;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HolaMoviles.Servicios
{
    public class ServicioPersonas
    {
        private const string url = "https://remotecoffee.azurewebsites.net/api/";

        public async Task<Persona[]> Obtener()
        {
            if (CrossConnectivity.Current.IsConnected == false)
            {
                return new Persona[0];
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                var json = await httpClient.GetStringAsync("Employees").ConfigureAwait(false);

                var resultado = JsonConvert.DeserializeObject<Persona[]>(json);

                return resultado;
            }
        }

        public async Task<Persona[]> ObtenerConCodigo()
        {
            if (CrossConnectivity.Current.IsConnected == false)
            {
                return new Persona[0];
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                var response = await httpClient.GetAsync("Employees").ConfigureAwait(false);

                Debug.WriteLine(response);

                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return new Persona[0];
                }

                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Persona[]>(json);

                return resultado;
            }
        }
    }
}
