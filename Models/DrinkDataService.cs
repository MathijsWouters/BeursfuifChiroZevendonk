using System.Text.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;
using Beursfuif.Models.Beursfuif.Models;
using System.Diagnostics;

namespace Beursfuif.Models
{
    public class DrinkDataService
    {
        public async Task<bool> SaveDrinksLayoutAsync(ObservableCollection<Drink> drinks, string filename)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                Debug.WriteLine(folderPath);
                var filePath = Path.Combine(folderPath, $"{filename}.json");

                var json = JsonSerializer.Serialize(drinks, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<string> GetSavedLayouts()
        {
            var folderPath = FileSystem.AppDataDirectory;
            var files = Directory.EnumerateFiles(folderPath, "*.json");
            return files.Select(Path.GetFileNameWithoutExtension);
        }
        public async Task<ObservableCollection<Drink>> LoadDrinksLayoutAsync(string filename)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var filePath = Path.Combine(folderPath, $"{filename}.json");

                var json = await File.ReadAllTextAsync(filePath);
                var drinks = JsonSerializer.Deserialize<ObservableCollection<Drink>>(json);
                return drinks ?? new ObservableCollection<Drink>();
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                return new ObservableCollection<Drink>();
            }
        }
        public async Task<bool> DeleteLayoutAsync(string filename)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var filePath = Path.Combine(folderPath, $"{filename}.json");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                return false;
            }
        }
        public async Task<List<DrinkSalesData>> LoadOrCreateSalesDataAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                var salesData = JsonSerializer.Deserialize<List<DrinkSalesData>>(json);
                return salesData ?? new List<DrinkSalesData>();
            }

            return new List<DrinkSalesData>();
        }
        public async Task<bool> UpdateAndSaveSalesDataAsync(string filename, string drinkName, int quantitySold, decimal income)
        {
            var salesDataList = await LoadOrCreateSalesDataAsync(filename);
            var drinkData = salesDataList.FirstOrDefault(d => d.DrinkName == drinkName);

            if (drinkData != null)
            {
                // Update existing data
                drinkData.TotalSold += quantitySold;
                drinkData.TotalIncome += income;
            }
            else
            {
                // Add new data entry
                salesDataList.Add(new DrinkSalesData
                {
                    DrinkName = drinkName,
                    TotalSold = quantitySold,
                    TotalIncome = income
                });
            }

            // Save updated list back to file
            return await SaveSalesDataAsync(filename, salesDataList);
        }

        private async Task<bool> SaveSalesDataAsync(string filename, List<DrinkSalesData> salesData)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var filePath = Path.Combine(folderPath, filename);

                var json = JsonSerializer.Serialize(salesData, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<FiveMinuteDrinkSalesData>> LoadOrCreateFiveMinuteSalesDataAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                var data = JsonSerializer.Deserialize<List<FiveMinuteDrinkSalesData>>(json);
                return data ?? new List<FiveMinuteDrinkSalesData>();
            }

            return new List<FiveMinuteDrinkSalesData>();
        }

        public async Task UpdateAndSaveFiveMinuteSalesDataAsync(string filename, string drinkName, int quantitySold)
        {
            var data = await LoadOrCreateFiveMinuteSalesDataAsync(filename);
            var drinkData = data.FirstOrDefault(d => d.DrinkName == drinkName);

            if (drinkData != null)
            {
                // Update existing data
                drinkData.QuantitySoldLastFiveMinutes += quantitySold;
            }
            else
            {
                // Add new data entry
                data.Add(new FiveMinuteDrinkSalesData
                {
                    DrinkName = drinkName,
                    QuantitySoldLastFiveMinutes = quantitySold
                });
            }

            // Save updated list back to file
            await SaveFiveMinuteSalesDataAsync(filename, data);
        }

        private async Task SaveFiveMinuteSalesDataAsync(string filename, List<FiveMinuteDrinkSalesData> data)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task ClearFiveMinuteSalesDataAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Optionally, create a new empty file
            await File.WriteAllTextAsync(filePath, "[]");
        }
    }
}
