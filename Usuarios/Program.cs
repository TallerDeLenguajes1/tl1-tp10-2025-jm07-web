using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Usuarios;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string url = "https://jsonplaceholder.typicode.com/users/";

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        
        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Usuario>? usuarios = JsonSerializer.Deserialize<List<Usuario>>(json, opciones);

        if (usuarios != null)
        {
            Console.WriteLine("Primeros 5 usuarios:\n");

            for (int i = 0; i < 5 && i < usuarios.Count; i++)
            {
                var u = usuarios[i];
                Console.WriteLine($"Nombre: {u.Name}");
                Console.WriteLine($"Email: {u.Email}");
                Console.WriteLine($"Domicilio: {u.Address}\n");
            }
        }
        else
        {
            Console.WriteLine("No se pudieron cargar los usuarios.");
        }
    }
}
