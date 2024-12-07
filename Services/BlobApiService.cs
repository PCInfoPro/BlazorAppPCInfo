using System.Net.Http.Headers;
using UtilInfoPC.Models;
using System.Text.Json;
using NLog;
using System.Net;
using Microsoft.AspNetCore.Mvc.Routing;

namespace INFOPC.Services
{
    public class BlobApiService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly HttpClient _httpClient = new HttpClient();
        private static AuthToken? token = null;
        private static LoginUser user = new LoginUser 
        {
            Username = "admin",
            Password = "password"
        };

        public BlobApiService(string uri)
        {
            _httpClient.BaseAddress = new  Uri(uri);
        }

        public class URL
        {
            public string url { get; set; }
        }

        public static async Task<URL?> GetBlobUrlsAsync(string blobName)
        {
            if (token == null)
            _ = await Login(user); 
            try
            {
                return await _httpClient.GetFromJsonAsync<URL>($"{_httpClient.BaseAddress}/Blobs/sas/{blobName}");
            }
            catch (WebException ex)
            {
                return new URL { url = string.Empty};
            }
            catch (HttpRequestException ex)
            {
                return new URL { url = string.Empty};
            }
            catch (Exception ex)
            {
                return new URL { url = string.Empty};
            }
        }

        // Método para hacer login y obtener el token JWT
        public static async Task<AuthToken> Login(LoginUser user)
        {
            if(token == null)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}/Auth/login");
                request.Content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                
                try
                {
                    var response = await _httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        token = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthToken>(jsonString); // O usa System.Text.Json.JsonSerializer

                        logger.Info($"Token auth recibido: {token}");
                        // Agregar el token JWT al encabezado de la solicitud
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                        return token;
                    }
                    else
                    {
                        logger.Warn($"Error: {response.StatusCode}");
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("Error en carga de datos: " + ex.Message + ex.StackTrace);
                }
            }

            return token; // Si falló el login, devuelve null
        }
    }
}
