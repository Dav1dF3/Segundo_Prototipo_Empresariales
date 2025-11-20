using GUICliente2.Model;
using GUICliente2.Service.ClienteInstrumentos.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUICliente2.Service
{
    public class ServicioAmplificador
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "amplificadores";

        public ServicioAmplificador(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:admin"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ClienteAmplificadoresApp");
        }

        private static JsonSerializerOptions Options => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Crear un nuevo amplificador
        public async Task<AmplificadorDTO?> CrearAmplificador(AmplificadorDTO nuevoAmplificador)
        {
            var json = JsonSerializer.Serialize(nuevoAmplificador, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Endpoint}/", content);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AmplificadorDTO>(body, Options);
        }

        // Listar todos los amplificadores
        public async Task<List<AmplificadorDTO>> ListarAmplificadores()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/");
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AmplificadorDTO>>(body, Options) ?? new List<AmplificadorDTO>();
        }

        // Obtener amplificador por id
        public async Task<AmplificadorDTO?> ObtenerAmplificador(int id)
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AmplificadorDTO>(body, Options);
        }

        // Actualizar un amplificador
        public async Task<bool> ActualizarAmplificador(int codigo, object amplificador)
        {
            var json = JsonSerializer.Serialize(amplificador, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"amplificadores/{codigo}", content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.OK;
        }



        // Eliminar amplificador
        public async Task<bool> EliminarAmplificador(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            // Para delete Spring devuelve 204, checa conf actual
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        // Buscar amplificadores por marca
        public async Task<List<AmplificadorDTO>> BuscarPorMarca(string marca)
        {
            // Si la marca es opcional, puedes quitar el parámetro si está vacío
            var url = $"{Endpoint}/buscar" + (!string.IsNullOrEmpty(marca) ? $"?marca={marca}" : "");
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AmplificadorDTO>>(body, Options) ?? new List<AmplificadorDTO>();
        }

        // Listar guitarras con amplificadores
        public async Task<List<GuitarrasConAmplificadorDTO>> ListarGuitarrasYAmplificadores(string? marcaAmplificador = null)
        {
            var url = $"{Endpoint}/listado/guitarras-amplificadores" +
                        (!string.IsNullOrEmpty(marcaAmplificador) ? $"?marcaAmplificador={marcaAmplificador}" : "");
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GuitarrasConAmplificadorDTO>>(body, Options) ?? new List<GuitarrasConAmplificadorDTO>();
        }
    }
}