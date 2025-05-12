using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUp.Models;
using SaveUp.Services;
using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;

namespace SaveUp.ViewModels
{
    public partial class SavingsListViewModel : ObservableObject
    {
        private readonly SavingService _savingService;

        [ObservableProperty]
        private ObservableCollection<SavingItem> savings;

        [ObservableProperty]
        private double totalSavings;

        [ObservableProperty]
        private string targetAmount;

        [ObservableProperty]
        private Chart savingsProgressChart;

        [ObservableProperty]
        private double progressPercentage;

        [ObservableProperty]
        private double remainingPercentage;

        public SavingsListViewModel(SavingService savingService)
        {
            _savingService = savingService;
            LoadSavings();
        }

        private async void LoadSavings()
        {
            Savings = await _savingService.LoadSavingsAsync();
            UpdateTotalSavings();
            UpdateChart();
        }

        [RelayCommand]
        private async Task Delete(SavingItem item)
        {
            if (item != null)
            {
                await _savingService.DeleteSavingAsync(Savings, item);
                UpdateTotalSavings();
                UpdateChart();
            }
        }

        [RelayCommand]
        private async Task DeleteAll()
        {
            await _savingService.DeleteAllSavingsAsync(Savings);
            UpdateTotalSavings();
            UpdateChart();
        }

        private void UpdateTotalSavings()
        {
            TotalSavings = Savings?.Sum(s => s.Price) ?? 0;
        }

        private void UpdateChart()
        {
            if (!double.TryParse(TargetAmount, out double target) || target <= 0)
            {
                SavingsProgressChart = null;
                ProgressPercentage = 0;
                RemainingPercentage = 0;
                return;
            }

            double progress = Math.Min(1.0, TotalSavings / target);
            double remaining = TotalSavings >= target ? 0.0 : 1.0 - progress;

            ProgressPercentage = progress * 100;
            RemainingPercentage = remaining * 100;

            var entries = new[]
            {
            new ChartEntry((float)(progress * 100))
            {
                Color = SKColor.Parse("#88B04B")
            },
            new ChartEntry((float)(remaining * 100))
            {
                Color = SKColor.Parse("#FF6F61")
            }
        };

            SavingsProgressChart = new DonutChart
            {
                Entries = entries,
                LabelTextSize = 30f,
                LabelColor = SKColors.White,
                BackgroundColor = SKColors.Transparent,
                Margin = 10,
                HoleRadius = 0.6f,
                LabelMode = LabelMode.None
            };
        }

        partial void OnTargetAmountChanged(string value)
        {
            UpdateChart();
        }
    }

}