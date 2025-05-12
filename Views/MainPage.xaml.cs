using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace SaveUp.Views;

public partial class MainPage : ContentPage
{
    public ICommand GoToAddSavingCommand { get; }
    public ICommand GoToListCommand { get; }

    public MainPage()
    {
        InitializeComponent();
        GoToAddSavingCommand = new AsyncRelayCommand(async () => await Shell.Current.GoToAsync("//AddSavingPage"));
        GoToListCommand = new AsyncRelayCommand(async () => await Shell.Current.GoToAsync("//SavingsListPage"));
        BindingContext = this;
    }

}