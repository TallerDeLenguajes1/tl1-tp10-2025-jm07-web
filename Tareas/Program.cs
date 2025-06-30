using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Tareas;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        await ObtenerTareasAsync();
    }

    private static async Task ObtenerTareasAsync()
    {
        string url = "https://jsonplaceholder.typicode.com/todos/";

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Tarea>? tareas = JsonSerializer.Deserialize<List<Tarea>>(json, opciones);

        if (tareas != null)
        {
            Console.WriteLine($"Se cargaron {tareas.Count} tareas.\n");

            var pendientes = tareas.FindAll(t => !t.Completed);
            var completadas = tareas.FindAll(t => t.Completed);

            Console.WriteLine("TAREAS PENDIENTES:");
            foreach (var tarea in pendientes)
            {
                Console.WriteLine($"- {tarea.Title}");
            }

            Console.WriteLine("\nTAREAS COMPLETADAS:");
            foreach (var tarea in completadas)
            {
                Console.WriteLine($"- {tarea.Title}");
            }

            string salida = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync("tareas.json", salida);

            Console.WriteLine("\nGuardado exitoso.");
        }
        else
        {
            Console.WriteLine("No se pudieron cargar las tareas.");
        }
    }
}