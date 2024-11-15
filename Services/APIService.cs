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
                ToggleLoading(false);
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
            ToggleLoading(true);
            Computer result = await _httpClient.GetFromJsonAsync<Computer>($"{_httpClient.BaseAddress}/Computers/{id}");
            ToggleLoading(false);
            return result;
        }

        // Crear un ordenador
        public static async Task<Computer> PostComputers(Computer computer)
        {
            try
            {
                ToggleLoading(true);
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Computers", computer);
                ToggleLoading(false);
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
            try
            {
                ToggleLoading(true);
                List<Processor> result = await _httpClient.GetFromJsonAsync<List<Processor>>($"{_httpClient.BaseAddress}/Processors");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de procesadores.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetProcessors.");
                throw;
            }
        }

        // Crear procesador
        public static async Task<bool> PostProcessors(Processor processor)
        {
            try
            {
                ToggleLoading(true);
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Processors", processor);
                ToggleLoading(false);
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

        // Obtener todas las marcas
        public static async Task<List<Brand>> GetBrands()
        {
            try
            {
                ToggleLoading(true);
                List<Brand> result = await _httpClient.GetFromJsonAsync<List<Brand>>($"{_httpClient.BaseAddress}/Brands");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de marcas.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetBrands.");
                throw;
            }
        }

        // Crear marca
        public static async Task<bool> PostBrands(Brand brand)
        {
            try
            {
                ToggleLoading(true);
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Brands", brand);
                ToggleLoading(false);
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

        // Obtener todas las RAMs
        public static async Task<List<RAM>> GetRAMs()
        {
            try
            {
                ToggleLoading(true);
                List<RAM> result = await _httpClient.GetFromJsonAsync<List<RAM>>($"{_httpClient.BaseAddress}/Rams");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de RAMs.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetRAMs.");
                throw;
            }
        }

        // Crear RAM
        public static async Task<bool> PostRAMs(RAM ram)
        {
            try
            {
                ToggleLoading(true);
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Rams", ram);
                ToggleLoading(false);
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

        // Obtener todos los almacenamientos
        public static async Task<List<HardDrive>> GetStorages()
        {
            try
            {
                ToggleLoading(true);
                List<HardDrive> result = await _httpClient.GetFromJsonAsync<List<HardDrive>>($"{_httpClient.BaseAddress}/Storages");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de almacenamientos.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetStorages.");
                throw;
            }
        }

        // Crear almacenamiento
        public static async Task<bool> PostStorages(HardDrive hardDrive)
        {
            try
            {
                ToggleLoading(true);
                var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Storages", hardDrive);
                ToggleLoading(false);
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

        // Obtener todos los ordenadores en formato visual
        public static async Task<List<ComputersSimpleDetail>> GetComputersSimpleDetails()
        {
            try
            {
                ToggleLoading(true);
                List<ComputersSimpleDetail> result = await _httpClient.GetFromJsonAsync<List<ComputersSimpleDetail>>($"{_httpClient.BaseAddress}/ComputersSimpleDetails");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de los ordenadores en formato visual.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetComputersSimpleDetails.");
                throw;
            }
        }

        // Obtener un ordenador en formato visual
        public static async Task<ComputersSimpleDetail> GetComputersSimpleDetail(int id)
        {
            ToggleLoading(true);
            ComputersSimpleDetail result = await _httpClient.GetFromJsonAsync<ComputersSimpleDetail>($"{_httpClient.BaseAddress}/ComputersSimpleDetails/{id}");
            ToggleLoading(false);
            return result;
        }

        // Obtener todos los ordenadores en formato visual
        public static async Task<List<ComputerExtendDetail>> GetComputerExtendDetails()
        {
            try
            {
                ToggleLoading(true);
                List<ComputerExtendDetail> result = await _httpClient.GetFromJsonAsync<List<ComputerExtendDetail>>($"{_httpClient.BaseAddress}/ComputerExtendDetails");
                ToggleLoading(false);
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex, "Error al obtener la lista de los ordenadores en formato visual extendido.");
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inesperado en la solicitud de GetComputerExtendDetails.");
                throw;
            }
        }

        // Obtener un ordenador en formato visual extendido
        public static async Task<ComputerExtendDetail> GetComputerExtendDetail(int id)
        {
            ToggleLoading(true);
            ComputerExtendDetail result = await _httpClient.GetFromJsonAsync<ComputerExtendDetail>($"{_httpClient.BaseAddress}/ComputerExtendDetails/{id}");
            ToggleLoading(false);
            return result;
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
                    ToggleLoading(true);
                    var response = await _httpClient.SendAsync(request);
                    ToggleLoading(false);

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
