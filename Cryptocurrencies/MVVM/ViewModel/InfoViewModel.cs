using Cryptocurrencies.Core;
using Cryptocurrencies.MVVM.Model;

namespace Cryptocurrencies.MVVM.ViewModel;

public class InfoViewModel : ObservableObject
{
    private Cryptocurrency? _selectedCryptocurrency;
    public Cryptocurrency? SelectedCryptocurrency
    {
        get => _selectedCryptocurrency;
        set
        {
            _selectedCryptocurrency = value;
            OnPropertyChanged();
        }
    }
}