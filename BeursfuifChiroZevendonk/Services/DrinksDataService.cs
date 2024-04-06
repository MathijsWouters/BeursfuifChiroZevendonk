using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.Services
{
    public class DrinksDataService
    {
        public ObservableCollection<Drink> Drinks { get; private set; } = new ObservableCollection<Drink>();

        private const string previousFiveMinuteDataFile = "five_minute_drink_data.json";
        private const string CurrentFiveMinuteDataFile = "current_five_minute_drink_data.json";
        public void UpdateDrink(Drink updatedDrink)
        {
            var drink = Drinks.FirstOrDefault(d => d.Number == updatedDrink.Number); 
            if (drink != null)
            {
                drink.Name = updatedDrink.Name;
                drink.MinPrice = updatedDrink.MinPrice;
                drink.MaxPrice = updatedDrink.MaxPrice;
            }
        }

        public async Task<bool> SaveDrinksLayoutAsync(ObservableCollection<Drink> drinks, string filename)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var formattedFilename = $"{filename}_layout.json"; 
                var filePath = Path.Combine(folderPath, formattedFilename);

                var json = JsonSerializer.Serialize(drinks, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<string> GetSavedLayouts()
        {
            var folderPath = FileSystem.AppDataDirectory;
            var files = Directory.EnumerateFiles(folderPath, "*_layout.json"); 
            return files.Select(file => Path.GetFileNameWithoutExtension(file).Replace("_layout", ""));
        }
        public async Task<ObservableCollection<Drink>> LoadDrinksLayoutAsync(string layoutName)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var filename = $"{layoutName}_layout.json";
                var filePath = Path.Combine(folderPath, filename);

                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<ObservableCollection<Drink>>(json) ?? new ObservableCollection<Drink>();
            }
            catch
            {
                return new ObservableCollection<Drink>();
            }
        }
        public async Task<bool> DeleteLayoutAsync(string layoutName)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
                var filename = $"{layoutName}_layout.json";
                var filePath = Path.Combine(folderPath, filename);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private async Task<List<DrinkSalesData>> LoadOrCreateSalesDataAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<DrinkSalesData>>(json) ?? new List<DrinkSalesData>();
            }

            return new List<DrinkSalesData>();
        }

        public async Task UpdateAndSaveSalesDataAsync(string filename, List<ReceiptItem> receiptItems)
        {
            var salesDataList = await LoadOrCreateSalesDataAsync(filename);
            foreach (var item in receiptItems)
            {
                var drinkData = salesDataList.FirstOrDefault(d => d.DrinkName == item.DrinkName);
                if (drinkData != null)
                {
                    drinkData.TotalSold += item.Quantity;
                    drinkData.TotalIncome += item.TotalPrice;
                }
                else
                {
                    salesDataList.Add(new DrinkSalesData
                    {
                        DrinkName = item.DrinkName,
                        TotalSold = item.Quantity,
                        TotalIncome = item.TotalPrice
                    });
                }
            }
            await SaveSalesDataAsync(filename, salesDataList);
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save sales data: {ex}");
                return false;
            }
        }

        public async Task ConvertSalesDataToExcelAsync(string sourceFileName, string targetFileName)
        {
            var salesDataList = await LoadOrCreateSalesDataAsync(sourceFileName);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sales Data");
                worksheet.Cell("A1").Value = "Drink Name";
                worksheet.Cell("B1").Value = "Total Sold";
                worksheet.Cell("C1").Value = "Total Income";

                decimal totalIncome = 0;
                int currentRow = 2; 
                foreach (var data in salesDataList)
                {
                    worksheet.Cell($"A{currentRow}").Value = data.DrinkName;
                    worksheet.Cell($"B{currentRow}").Value = data.TotalSold;
                    worksheet.Cell($"C{currentRow}").Value = data.TotalIncome;
                    totalIncome += data.TotalIncome; 
                    currentRow++;
                }
                worksheet.Cell($"A{currentRow}").Value = "Total Income";
                worksheet.Cell($"C{currentRow}").Value = totalIncome;
                worksheet.Range($"A{currentRow}:B{currentRow}").Merge(); 

                var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                var excelFilePath = Path.Combine(downloadsPath, targetFileName);
                if (!Directory.Exists(downloadsPath))
                {
                    Directory.CreateDirectory(downloadsPath);
                }
                workbook.SaveAs(excelFilePath);
                var jsonFilePath = Path.Combine(FileSystem.AppDataDirectory, sourceFileName);
                if (File.Exists(jsonFilePath))
                {
                    File.Delete(jsonFilePath);
                }
            }
        }

        public async Task DeleteSalesDataAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Debug.WriteLine($"Deleted file: {filePath}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting file {filename}: {ex.Message}");
                }
            }
        }
        public async Task InitializeFiveMinuteSalesDataWithRandomValuesAsync()
        {
            var random = new Random();
            var initialData = Drinks.Select(drink => new FiveMinuteDrinkSalesData
            {
                DrinkName = drink.Name,
                QuantitySoldLastFiveMinutes = random.Next(5, 15)
            }).ToList();
            var filePath = Path.Combine(FileSystem.AppDataDirectory, previousFiveMinuteDataFile);
            var json = JsonSerializer.Serialize(initialData);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task InitializeCurrentSalesDataAsync()
        {
            var currentSalesData = Drinks.Select(drink => new FiveMinuteDrinkSalesData
            {
                DrinkName = drink.Name,
                QuantitySoldLastFiveMinutes = 0
            }).ToList();
            var filePath = Path.Combine(FileSystem.AppDataDirectory, CurrentFiveMinuteDataFile);
            var json = JsonSerializer.Serialize(currentSalesData);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task ProcessFiveMinuteSalesDataAsync(List<Drink> drinks)
        {
            var previousSalesFilePath = Path.Combine(FileSystem.AppDataDirectory, previousFiveMinuteDataFile);
            var currentSalesFilePath = Path.Combine(FileSystem.AppDataDirectory, CurrentFiveMinuteDataFile);

            var previousSalesDataJson = await File.ReadAllTextAsync(previousSalesFilePath);
            var currentSalesDataJson = await File.ReadAllTextAsync(currentSalesFilePath);

            var previousSalesData = JsonSerializer.Deserialize<List<FiveMinuteDrinkSalesData>>(previousSalesDataJson) ?? new List<FiveMinuteDrinkSalesData>();
            var currentSalesData = JsonSerializer.Deserialize<List<FiveMinuteDrinkSalesData>>(currentSalesDataJson) ?? new List<FiveMinuteDrinkSalesData>();

            foreach (var drink in drinks)
            {
                var currentSale = currentSalesData.FirstOrDefault(c => c.DrinkName == drink.Name);
                var previousSale = previousSalesData.FirstOrDefault(p => p.DrinkName == drink.Name);
                if (currentSale != null && previousSale != null)
                {
                    var percentageChange = CalculatePercentageChange(previousSale.QuantitySoldLastFiveMinutes, currentSale.QuantitySoldLastFiveMinutes);
                    var adjustment = DetermineAdjustmentMagnitude(percentageChange, 0.25m); 
                    var newPrice = Math.Max(drink.MinPrice, Math.Min(drink.MaxPrice, drink.CurrentPrice + adjustment));
                    if (drink.CurrentPrice != newPrice)
                    {
                        drink.CurrentPrice = newPrice;
                    }
                    else
                    {
                        drink.LogCurrentPrice();
                    }
                }
            }
            await ResetCurrentSalesDataAsync();
        }
        public async Task ResetCurrentSalesDataAsync()
        {
            var currentSalesFilePath = Path.Combine(FileSystem.AppDataDirectory, CurrentFiveMinuteDataFile);
            var currentSalesDataJson = await File.ReadAllTextAsync(currentSalesFilePath);
            var currentSalesData = JsonSerializer.Deserialize<List<FiveMinuteDrinkSalesData>>(currentSalesDataJson) ?? new List<FiveMinuteDrinkSalesData>();
            var previousSalesFilePath = Path.Combine(FileSystem.AppDataDirectory, previousFiveMinuteDataFile);
            await File.WriteAllTextAsync(previousSalesFilePath, JsonSerializer.Serialize(currentSalesData));
            var resetData = Drinks.Select(drink => new FiveMinuteDrinkSalesData
            {
                DrinkName = drink.Name,
                QuantitySoldLastFiveMinutes = 0
            }).ToList();
            await File.WriteAllTextAsync(currentSalesFilePath, JsonSerializer.Serialize(resetData));
        }
        public async Task UpdateCurrentFiveMinuteSalesDataAsync(List<ReceiptItem> receiptItems)
        {
            var currentSalesDataPath = Path.Combine(FileSystem.AppDataDirectory, CurrentFiveMinuteDataFile);
            var currentSalesDataJson = await File.ReadAllTextAsync(currentSalesDataPath);
            var currentSalesData = JsonSerializer.Deserialize<List<FiveMinuteDrinkSalesData>>(currentSalesDataJson) ?? new List<FiveMinuteDrinkSalesData>();

            foreach (var item in receiptItems)
            {
                var drinkData = currentSalesData.FirstOrDefault(d => d.DrinkName == item.DrinkName);
                if (drinkData != null)
                {
                    drinkData.QuantitySoldLastFiveMinutes += item.Quantity;
                }
                else
                {
                    currentSalesData.Add(new FiveMinuteDrinkSalesData
                    {
                        DrinkName = item.DrinkName,
                        QuantitySoldLastFiveMinutes = item.Quantity
                    });
                }
            }

            await SaveFiveMinuteSalesDataAsync(CurrentFiveMinuteDataFile, currentSalesData);
        }
        private static async Task SaveFiveMinuteSalesDataAsync(string filename, List<FiveMinuteDrinkSalesData> salesData)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);
            try
            {
                var json = JsonSerializer.Serialize(salesData, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save five-minute sales data: {ex}");
            }
        }
        public async Task DeleteFileAsync(string filename)
        {
            var folderPath = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folderPath, filename);

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to delete file {filename}: {ex}");
                }
            }
        }
        private decimal CalculatePercentageChange(int previousSales, int currentSales)
        {
            if (previousSales == 0 && currentSales == 0)
            {
                return -100; 
            }
            else if (previousSales == 0)
            {
                return 100;
            }
            else
            {
                return ((decimal)(currentSales - previousSales) / previousSales) * 100;
            }
        }

        private static decimal DetermineAdjustmentMagnitude(decimal percentageChange, decimal interval)
        {
            var random = new Random();
            var chance = random.Next(100);

            if (percentageChange == -100)
            {
                return chance < 80 ? -interval : 0;
            }
            else if (percentageChange <= -15)
            {
                if (chance < 55) return -interval;
                else if (chance < 70) return -2 * interval;
                else return 0;
            }
            else if (percentageChange >= 15)
            {
                if (chance < 55) return interval;
                else if (chance < 70) return 2 * interval;
                else return 0;
            }
            else
            {
                if (chance < 42.5) return interval; 
                else if (chance < 85) return -interval; 
                else return 0; 
            }
        }
        public async Task SaveHistoricalPricesAsync()
        {
            var drinksData = Drinks.Select(drink => new
            {
                DrinkName = drink.Name,
                Prices = drink.HistoricalPrices.Skip(12).ToList() 
            }).ToList();

            var json = JsonSerializer.Serialize(drinksData, new JsonSerializerOptions { WriteIndented = true });
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "historical_drink_prices.json");
            await File.WriteAllTextAsync(filePath, json);
        }

    }

}
