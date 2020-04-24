namespace Lands.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = "Por favor active la conexión a Internet"
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = "Revisa tu conexión a Internet"
                };
            }

            return new Response
            {
                IsSucces = true,
                Message = "Oki Doki"
            };
        }

        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync(
                    "Token",
                    new StringContent(string.Format(
                        "grant_type=password&username={0}&password={1}", username, password),
                        Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;
            }
            catch
            {

                return null;
            }
        }

        public async Task<Response> GetList<T>(string urlBase, string prefijo, string controlador)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefijo}{controlador}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucces = false,
                        Message = answer
                    };
                }

                var lista = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSucces = true,
                    Result = lista
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
            }
        }
    }
}
