using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UtilInfoPC.Models;

namespace INFOPC.Services
{
    public class ComputerService
    {
        private readonly HttpClient _httpClient;

        public ComputerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los ordenadores
        public async Task<List<Computer>> GetOrdenadores(string token)
        {
            // Agregar el token JWT al encabezado de la solicitud
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetFromJsonAsync<List<Computer>>("computers");
        }

        // Método para hacer login y obtener el token JWT
        public async Task<string> Login(LoginUser user)
        {
            var response = await _httpClient.PostAsJsonAsync($"Auth/login", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthToken>();
                return result?.Token; // Devuelve el token JWT
            }

            return null; // Si falló el login, devuelve null
        }
    }
}
