using System.Windows;
using System.Windows.Input;
using Cryptocurrencies.Core;

namespace Cryptocurrencies.Helpers;

public static class WindowCommands
{
    public static ICommand CloseCommand { get; } = new RelayCommand(_ => OnCloseButtonClick());
    public static ICommand MinimizeCommand { get; } = new RelayCommand(_ => OnMinimizeButtonClick());
    public static ICommand RollUpCommand { get; } = new RelayCommand(_ => OnRollUpButtonClick());

    private static void OnCloseButtonClick() => Application.Current.Shutdown();

    private static void OnMinimizeButtonClick()
    {
        if (Application.Current.MainWindow is null) return;
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }

    private static void OnRollUpButtonClick()
    {
        if (Application.Current.MainWindow is null) return;
        Application.Current.MainWindow.WindowState =
            Application.Current.MainWindow.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
    }
}