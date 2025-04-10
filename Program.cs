using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
  static async Task Main()
  {
    await ListPokemonsAsync();
  }

  static async Task ListPokemonsAsync()
  {
    string url = "https://pokeapi.co/api/v2/pokemon?limit=10"; // Fetch 10 Pokémons

    using HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
      string json = await response.Content.ReadAsStringAsync();
      JObject data = JObject.Parse(json);

      foreach (var item in data["results"])
      {
        Console.WriteLine($"Name: {item["name"]}");
      }
    }
    else
    {
      Console.WriteLine("Error fetching data.");
    }
  }
}