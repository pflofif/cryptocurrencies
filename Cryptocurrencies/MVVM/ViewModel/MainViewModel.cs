using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptocurrencies.Core;
using Cryptocurrencies.MVVM.Model;
using Cryptocurrencies.MVVM.View;
using Cryptocurrencies.Services;

namespace Cryptocurrencies.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private readonly ICryptoApiService _apiService;
    private HomeViewModel HomeViewModel { get; }
    private InfoViewModel InfoViewModel { get; }
    private object _currentView = null!;
    private string _searchQuery = "";
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand InfoViewCommand { get; }
    public ICommand SearchCommand { get; }

    public MainViewModel(HomeViewModel homeViewModel, InfoViewModel infoViewModel, ICryptoApiService apiService)
    {
        HomeViewModel = homeViewModel;
        InfoViewModel = infoViewModel;
        _apiService = apiService;

        CurrentView = HomeViewModel;

        HomeViewCommand = new RelayCommand(_ => CurrentView = HomeViewModel);
        InfoViewCommand = new RelayCommand(_ => CurrentView = InfoViewModel);
        SearchCommand = new RelayCommand(async _ => await Search());
    }
    public object CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public string SearchQuery
    {
        get => _searchQuery; 
        set
        {
            _searchQuery = value;
            OnPropertyChanged();
        }
    }
    
    private async Task Search()
    {
        Cryptocurrency? crypto = await _apiService.GetCryptocurrencyAsync(SearchQuery);
        InfoViewModel.SelectedCryptocurrency = crypto;
        CurrentView = InfoViewModel;
    }
}