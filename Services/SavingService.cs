using System.Text.Json;
using System.Collections.ObjectModel;
using SaveUp.Models;

namespace SaveUp.Services
{
    public class SavingService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "savings.json");

        public async Task<ObservableCollection<SavingItem>> LoadSavingsAsync()
        {
            if (!File.Exists(_filePath))
                return new ObservableCollection<SavingItem>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<ObservableCollection<SavingItem>>(json) ?? new ObservableCollection<SavingItem>();
        }

        public async Task SaveSavingsAsync(ObservableCollection<SavingItem> savings)
        {
            var json = JsonSerializer.Serialize(savings);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task DeleteSavingAsync(ObservableCollection<SavingItem> savings, SavingItem item)
        {
            savings.Remove(item);
            await SaveSavingsAsync(savings);
        }

        public async Task DeleteAllSavingsAsync(ObservableCollection<SavingItem> savings)
        {
            savings.Clear();
            await SaveSavingsAsync(savings);
        }

        public async Task SaveSavingAsync(SavingItem item)
        {
            var savings = await LoadSavingsAsync();
            savings.Add(item);
            await SaveSavingsAsync(savings);
        }
    }

}