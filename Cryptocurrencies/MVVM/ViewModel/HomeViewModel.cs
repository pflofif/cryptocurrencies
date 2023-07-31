using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cryptocurrencies.Core;
using Cryptocurrencies.MVVM.Model;
using Cryptocurrencies.Services;

namespace Cryptocurrencies.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    private readonly ICryptoApiService _cryptoApiService;
    
    private ObservableCollection<Cryptocurrency> _topCurrencies;
    public ObservableCollection<Cryptocurrency> TopCurrencies
    {
        get => _topCurrencies;
        set
        {
            _topCurrencies = value;
            OnPropertyChanged();
        }
    }
    public HomeViewModel(ICryptoApiService cryptoApiService)
    {
        _cryptoApiService = cryptoApiService;

        LoadTopCurrenciesAsync();
    }
    
    private async Task LoadTopCurrenciesAsync()
    {
        try
        {
            const int numberOfCurrencies = 10; 
            List<Cryptocurrency>? response = await _cryptoApiService.GetTopCurrenciesAsync(numberOfCurrencies);
            if (response is not null)
            {
                TopCurrencies = new ObservableCollection<Cryptocurrency>(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading top currencies: {ex.Message}");
        }
    }
}