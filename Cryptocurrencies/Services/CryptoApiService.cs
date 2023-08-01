using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cryptocurrencies.MVVM.Model;
using Newtonsoft.Json;

namespace Cryptocurrencies.Services;

public interface ICryptoApiService
{
    Task<List<Cryptocurrency>?> GetTopCurrenciesAsync(int numberOfCurrencies);
    Task<Cryptocurrency?> GetCryptocurrencyAsync(string id);
}

public class CryptoApiService : ICryptoApiService
{
    private const string CapCoinBaseUrl = "https://api.coincap.io/v2";

    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Cryptocurrency?> GetCryptocurrencyAsync(string id)
    {
        try
        {
            string endpoint = $"{CapCoinBaseUrl}/assets/{id}";
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();
            CoinCapResponseSoloElement? coinCap = JsonConvert.DeserializeObject<CoinCapResponseSoloElement>(json);
            return coinCap?.Data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<List<Cryptocurrency>?> GetTopCurrenciesAsync(int numberOfCurrencies)
    {
        try
        {
            string endpoint = $"{CapCoinBaseUrl}/assets?limit={numberOfCurrencies}";
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<Cryptocurrency>().ToList();
            }

            string json = await response.Content.ReadAsStringAsync();
            CoinCapResponseListElements? coinCap = JsonConvert.DeserializeObject<CoinCapResponseListElements>(json);
            return coinCap?.Data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}