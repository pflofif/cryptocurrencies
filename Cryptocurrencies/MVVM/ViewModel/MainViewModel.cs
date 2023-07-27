using Cryptocurrencies.Core;
using Cryptocurrencies.MVVM.View;

namespace Cryptocurrencies.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private HomeViewModel HomeViewModel { get; }
    private InfoViewModel InfoViewModel { get; }
    private object _currentView;

    public RelayCommand HomeViewCommand { get; }
    public RelayCommand InfoViewCommand { get; }

    public MainViewModel()
    {
        InfoViewModel = new InfoViewModel();
        HomeViewModel = new HomeViewModel();
        
        CurrentView = HomeViewModel;

        HomeViewCommand = new RelayCommand(_ => CurrentView = HomeViewModel);
        InfoViewCommand = new RelayCommand(_ => CurrentView = InfoViewModel);
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
}