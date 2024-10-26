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
    public class APIService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly HttpClient _httpClient;

        public APIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los ordenadores
        public async Task<List<Computer>> GetComputers()
        {
            return await _httpClient.GetFromJsonAsync<List<Computer>>("http://localhost:5175/api/Computers");
        }

        // Obtener todos los ordenadores
        public async Task<bool> PostComputers(Computer computer)
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
        public async Task<List<Processor>> GetProcessors()
        {
            return await _httpClient.GetFromJsonAsync<List<Processor>>("http://localhost:5175/api/Processors");
        }

        // Obtener todos los ordenadores
        public async Task<bool> PostProcessors(Processor processor)
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
        public async Task<List<Brand>> GetBrands()
        {
            return await _httpClient.GetFromJsonAsync<List<Brand>>("http://localhost:5175/api/Brands");
        }

        // Obtener todos los ordenadores
        public async Task<bool> PostBrands(Brand brand)
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
        public async Task<List<RAM>> GetRAMs()
        {
            return await _httpClient.GetFromJsonAsync<List<RAM>>("http://localhost:5175/api/Rams");
        }

        // Obtener todos los ordenadores
        public async Task<bool> PostRAMs(RAM ram)
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

        // Método para hacer login y obtener el token JWT
        public async Task<AuthToken> Login(LoginUser user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5175/api/Auth/login");
            request.Content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            AuthToken token = new AuthToken();

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            token = JsonSerializer.Deserialize<AuthToken>(jsonString);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            return token;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }

            return null; // Si falló el login, devuelve null
        }
    }
}
