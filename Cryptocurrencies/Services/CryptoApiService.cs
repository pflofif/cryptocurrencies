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
}

public class CryptoApiService : ICryptoApiService
{
    private const string CapCoinBaseUrl = "https://api.coincap.io/v2";

    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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
            CoinCapResponse? coinCap = JsonConvert.DeserializeObject<CoinCapResponse>(json);
            return coinCap?.Data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}