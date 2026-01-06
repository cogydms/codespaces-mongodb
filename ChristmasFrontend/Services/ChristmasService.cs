using System.Net.Http.Json;
using System.Text.Json;
using ChristmasFrontend.Models;

namespace ChristmasFrontend.Services;

public class ChristmasService
{
    private readonly HttpClient client;
    private const string baseUrl = "http://localhost:5247/christmas";

    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ChristmasService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<ChristmasMarket?>> GetMarketsAsync()
        => await client.GetFromJsonAsync<List<ChristmasMarket>>(baseUrl);

    public async Task<ChristmasMarket?> GetMarketByIdAsync(string id)
        => await client.GetFromJsonAsync<ChristmasMarket>($"{baseUrl}/market/{id}",options);

    public async Task<Locations?> GetLocationByIdAsync(string id)
        => await client.GetFromJsonAsync<Locations>($"{baseUrl}/location/{id}", options);

    public async Task<List<ChristmasMarket>> SearchMarketAsync(string keyword)
        => await client.GetFromJsonAsync<List<ChristmasMarket>>(
            $"{baseUrl}/search?keyword={keyword}");

    public async Task CreateAsync(ChristmasMarket market)
        => await client.PostAsJsonAsync(baseUrl, market);

    public async Task UpdateAsync(string id, ChristmasMarket market)
        => await client.PutAsJsonAsync($"{baseUrl}/{market.Id}", market);

    public async Task DeleteAsync(string id)
        => await client.DeleteAsync($"{baseUrl}/{id}");

    public async Task<List<ChristmasMarket>> GetMarketsByDateAsync(DateTime date)
    {
        // baseUrl을 사용하고, JsonSerializerOptions 적용
        var response = await client.GetAsync($"{baseUrl}/bydate/{date:yyyy-MM-dd}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<ChristmasMarket>>(content, options)
               ?? new List<ChristmasMarket>();
    }
}
