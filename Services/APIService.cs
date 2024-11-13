using System.Net.Http.Headers;
using UtilInfoPC.Models;
using System.Text.Json;
using NLog;

namespace INFOPC.Services
{
    public class APIService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly HttpClient _httpClient = new HttpClient();
        private static AuthToken token = null;
        public static event Action<bool> OnLoadingChanged;

        public APIService(string uri)
        {
            _httpClient.BaseAddress = new  Uri(uri);
        }

        private static void ToggleLoading(bool isLoading)
        {
            OnLoadingChanged?.Invoke(isLoading);
        }

        // Obtener todos los ordenadores
        public static async Task<List<Computer>> GetComputers()
        {
            try
            {
                
                ToggleLoading(true);
                List<Computer> result = await _httpClient.GetFromJsonAsync<List<Computer>>($"{_httpClient.BaseAddress}/Computers");
                // ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de ordenadores.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetComputers.");
                throw;
            }
        }

        // Obtener un ordenador
        public static async Task<Computer> GetComputer(int id)
        {
            return await _httpClient.GetFromJsonAsync<Computer>($"{_httpClient.BaseAddress}/Computers/{id}");
        }

        // Obtener todos los ordenadores
        public static async Task<Computer> PostComputers(Computer computer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Computers", computer);
                var jsonString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(jsonString);;
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
            return null;
        }

        // Obtener todos los procesadores
        public static async Task<List<Processor>> GetProcessors()
        {
            return await _httpClient.GetFromJsonAsync<List<Processor>>($"{_httpClient.BaseAddress}/Processors");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostProcessors(Processor processor)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Processors", processor);
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
            return await _httpClient.GetFromJsonAsync<List<Brand>>($"{_httpClient.BaseAddress}/Brands");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostBrands(Brand brand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Brands", brand);
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
            return await _httpClient.GetFromJsonAsync<List<RAM>>($"{_httpClient.BaseAddress}/Rams");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostRAMs(RAM ram)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Rams", ram);
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
            return await _httpClient.GetFromJsonAsync<List<HardDrive>>($"{_httpClient.BaseAddress}/Storages");
        }

        // Obtener todos los ordenadores
        public static async Task<bool> PostStorages(HardDrive hardDrive)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Storages", hardDrive);
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
