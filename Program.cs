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
    // Para listar os 150 primeiros Pokemons (primeira geração)
    string url = "https://pokeapi.co/api/v2/pokemon?limit=150"; 

    using HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
      string json = await response.Content.ReadAsStringAsync();
      JObject data = JObject.Parse(json);

      foreach (var item in data["results"])
      {
        string pokemonUrl = item["url"].ToString();
        HttpResponseMessage pokemonResponse = await client.GetAsync(pokemonUrl);
        string pokemonJson = await pokemonResponse.Content.ReadAsStringAsync();
        JObject pokemonData = JObject.Parse(pokemonJson);

        Console.WriteLine($"Order: {pokemonData["id"]}, Name: {item["name"]}");
      }
    }
  }
}