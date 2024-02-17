using System.Text.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;

namespace Beursfuif.Models
{
    public class DrinkDataService
    {
        public async Task<bool> SaveDrinksLayoutAsync(ObservableCollection<Drink> drinks, string filename)
        {
            try
            {
                var folderPath = FileSystem.AppDataDirectory;
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

    }
}
