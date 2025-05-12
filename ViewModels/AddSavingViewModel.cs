using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUp.Models;
using SaveUp.Services;
using System.Threading.Tasks;
namespace SaveUp.ViewModels;

public partial class AddSavingViewModel : ObservableObject
{
    private readonly SavingService _savingService;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private string price;

    public AddSavingViewModel(SavingService savingService)
    {
        _savingService = savingService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Description) || !double.TryParse(Price, out double priceValue))
        {
            await Application.Current.MainPage.DisplayAlert("Fehler", "Bitte geben Sie eine Beschreibung und einen gültigen Preis ein.", "OK");
            return;
        }

        var item = new SavingItem
        {
            Description = Description,
            Price = priceValue,
            DateTime = DateTime.Now
        };

        await _savingService.SaveSavingAsync(item);
        await Application.Current.MainPage.DisplayAlert("Erfolg", "Eintrag gespeichert!", "OK");
        Description = string.Empty;
        Price = string.Empty;
    }

    [RelayCommand]
    private async Task GoToListAsync()
    {
        await Shell.Current.GoToAsync("//SavingsListPage");
    }

}