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
    public class ServicioInstrumento
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "instrumentos";

        public ServicioInstrumento(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"admin:admin"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ClienteInstrumentosApp");
        }

        private static JsonSerializerOptions Options => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Health Check
        public async Task<string> HealthCheckAsync()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/healthCheck");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Listar todos los instrumentos
        public async Task<List<Instrumento>> ListarInstrumentos()
        {
            var response = await _httpClient.GetAsync(Endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Instrumento>>(json, Options);
        }

        // Listar solo guitarras
        public async Task<List<Guitarra>> ListarGuitarras()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/guitarras");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Guitarra>>(json, Options);
        }

        // Listar solo teclados
        public async Task<List<Teclado>> ListarTeclados()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/teclados");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Teclado>>(json, Options);
        }

        // Buscar por código
        public async Task<Instrumento?> BuscarInstrumento(string codigo)
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/{codigo}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // Analizamos el JSON para saber qué tipo de instrumento es
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("type", out var typeElement))
                return JsonSerializer.Deserialize<Instrumento>(json, Options);

            string? type = typeElement.GetString()?.ToLower();

            return type switch
            {
                "teclado" => JsonSerializer.Deserialize<Teclado>(json, Options),
                "guitarra" => JsonSerializer.Deserialize<Guitarra>(json, Options),
                _ => JsonSerializer.Deserialize<Instrumento>(json, Options)
            };
        }


        // Agregar nuevo instrumento
        public async Task<bool> AgregarInstrumento(object instrumento)
        {
            var json = JsonSerializer.Serialize(instrumento, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Endpoint, content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.Created;
        }

        // Editar instrumento existente
        public async Task<bool> EditarInstrumento(string codigo, object instrumento)
        {
            var json = JsonSerializer.Serialize(instrumento, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{Endpoint}/{codigo}", content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.OK;
        }

        // Eliminar instrumento
        public async Task<bool> EliminarInstrumento(string codigo)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{codigo}");
            response.EnsureSuccessStatusCode();
            return response.StatusCode == HttpStatusCode.OK;
        }

        //Listar las fundas

        public async Task<List<Funda>> ListarFundas()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/guitarras/fundas");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Funda>>(json, Options);
        }


        // Agregar fundas a una guitarra
        public async Task<bool> AgregarFundas(string codigo, List<Funda> fundas)
        {
            var json = JsonSerializer.Serialize(fundas, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Endpoint}/guitarras/{codigo}/fundas", content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.Created;
        }

        //Buscar una funda
        public async Task<Funda> buscarFunda(string codigo, string codigoFunda)
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/guitarras/{codigo}/fundas/{codigoFunda}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Funda>(json, Options);
        }

        // Editar una funda
        public async Task<bool> EditarFunda(string codigo, string codigoFunda, Funda funda)
        {
            var json = JsonSerializer.Serialize(funda, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{Endpoint}/guitarras/{codigo}/fundas/{codigoFunda}", content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.OK;
        }

        // Eliminar funda
        public async Task<bool> EliminarFunda(string codigo, string codigoFunda)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/guitarras/{codigo}/fundas/{codigoFunda}");
            response.EnsureSuccessStatusCode();

            return response.StatusCode == HttpStatusCode.OK;
        }

        // Filtrar instrumentos
        public async Task<List<Instrumento>> FiltrarInstrumentos(FiltroInstrumentoDTO filtro)
        {
            var json = JsonSerializer.Serialize(filtro, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Endpoint}/filtrar", content);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Instrumento>>(body, Options);
        }

        public async Task<List<Guitarra>> FiltrarGuitarras(FiltroInstrumentoDTO filtro)
        {
            var json = JsonSerializer.Serialize(filtro, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Endpoint}/guitarras/filtrar", content);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Guitarra>>(body, Options);
        }

        public async Task<List<Funda>> FiltrarFundas(FiltroFundaDTO filtro)
        {
            var json = JsonSerializer.Serialize(filtro, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            MessageBox.Show(json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Endpoint}/guitarras/fundas/filtrar", content);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Funda>>(body, Options);
        }
    }
}
