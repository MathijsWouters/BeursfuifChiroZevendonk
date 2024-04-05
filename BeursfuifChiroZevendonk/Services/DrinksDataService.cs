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
                    // Handle any errors, such as logging or notifying the user
                }
            }
        }

    }
}
