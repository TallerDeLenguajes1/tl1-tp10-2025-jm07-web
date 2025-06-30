using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using MiWebApi;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    private static async Task Main()
    {
        await obtenerDatosApi();
    }

    private static async Task obtenerDatosApi()
    {
        string url = "https://catfact.ninja/facts";

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        CatFactResponse? resultado = JsonSerializer.Deserialize<CatFactResponse>(json, opciones);

        if (resultado?.Data != null)
        {
            Console.WriteLine("Curiosidades sobre gatos:\n");
            foreach (var fact in resultado.Data)
            {
                Console.WriteLine($"- {fact.Fact} (longitud: {fact.Length})");
            }

            string salidaJson = JsonSerializer.Serialize(resultado, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync("catfacts.json", salidaJson);
            Console.WriteLine("\nArchivo guardado exitosamente.");
        }
        else
        {
            Console.WriteLine("No se pudieron obtener datos de la API.");
        }
    }
}
