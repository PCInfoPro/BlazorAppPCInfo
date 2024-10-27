using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UtilInfoPC.Models;
using System.Text.Json;
using NLog;

namespace INFOPC.Services
{
    public static class APIService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly HttpClient _httpClient;
        private static AuthToken token = new AuthToken();

        static APIService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5175/api")
            };
        }

        // Obtener todos los ordenadores
        public static async Task<List<Computer>> GetComputers()
        {
            return await _httpClient.GetFromJsonAsync<List<Computer>>("http://localhost:5175/api/Computers");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostComputers(Computer computer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5175/api/Computers", computer);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    logger.Error("Error on PostComputers: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error on PostComputers: " + ex.Message + ex.StackTrace);
            }
            return false;
        }

        // Obtener todos los procesadores
        public static async Task<List<Processor>> GetProcessors()
        {
            return await _httpClient.GetFromJsonAsync<List<Processor>>("http://localhost:5175/api/Processors");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostProcessors(Processor processor)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5175/api/Processors", processor);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    logger.Error("Error on PostProcessors: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error on PostProcessors: " + ex.Message + ex.StackTrace);
            }
            return false;
        }

        // Obtener todos los procesadores
        public static async Task<List<Brand>> GetBrands()
        {
            return await _httpClient.GetFromJsonAsync<List<Brand>>("http://localhost:5175/api/Brands");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostBrands(Brand brand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5175/api/Brands", brand);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    logger.Error("Error on PostBrands: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error on PostBrands: " + ex.Message + ex.StackTrace);
            }
            return false;
        }

        // Obtener todos los procesadores
        public static async Task<List<RAM>> GetRAMs()
        {
            return await _httpClient.GetFromJsonAsync<List<RAM>>("http://localhost:5175/api/Rams");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostRAMs(RAM ram)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5175/api/Rams", ram);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    logger.Error("Error on PostRAMs: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error on PostRAMs: " + ex.Message + ex.StackTrace);
            }
            return false;
        }

        // Obtener todos los procesadores
        public static async Task<List<HardDrive>> GetStorages()
        {
            return await _httpClient.GetFromJsonAsync<List<HardDrive>>("http://localhost:5175/api/Storages");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostStorages(HardDrive hardDrive)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5175/api/Storages", hardDrive);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    logger.Error("Error on PostStorages: " + response.Content);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error on PostStorages: " + ex.Message + ex.StackTrace);
            }
            return false;
        }

        // Método para hacer login y obtener el token JWT
        public static async Task<AuthToken> Login(LoginUser user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5175/api/Auth/login");
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
                    // var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5175/api/Auth/login");
                    // request.Content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                    // var response = await _httpClient.SendAsync(request);
                

                
                    // var jsonString = await response.Content.ReadAsStringAsync();
                    // token = JsonSerializer.Deserialize<AuthToken>(jsonString);
                    // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

            return null; // Si falló el login, devuelve null
        }
    }
}
