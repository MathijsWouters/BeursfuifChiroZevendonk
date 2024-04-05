using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    }
}
