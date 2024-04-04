using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class ManageDrinksPageViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        private readonly DrinksDataService _drinksService;

        public ICommand DeleteDrinkCommand { get; private set; }
        public ICommand EditDrinkCommand { get; private set; }
        public ICommand NavigateToMainPageCommand { get; private set; }
        public ICommand SaveLayoutCommand { get; }
        public ICommand LoadLayoutCommand { get; }
        public ICommand DeleteLayoutCommand { get; }

        public ManageDrinksPageViewModel(DrinksDataService drinksService)
        {
            _drinksService = drinksService;
            DeleteDrinkCommand = new RelayCommand<Drink>(OnDeleteDrink);
            EditDrinkCommand = new RelayCommand<Drink>(OnEditDrink);
            NavigateToMainPageCommand = new RelayCommand(NavigateToMainPage);
            SaveLayoutCommand = new RelayCommand(OnSaveLayout);
            LoadLayoutCommand = new RelayCommand(OnLoadLayout);
            DeleteLayoutCommand = new RelayCommand(OnDeleteLayout);
        }

        private async void OnDeleteDrink(Drink drink)
        {
            // Ask the user for confirmation before deleting the drink
            bool isUserSure = await App.Current.MainPage.DisplayAlert(
                "Confirm",
                "Are you sure you want to delete this drink?",
                "Yes",
                "No"
            );

            if (isUserSure)
            {
                int deleteIndex = _drinksService.Drinks.IndexOf(drink);
                if (deleteIndex != -1)
                {
                  
                    _drinksService.Drinks.Remove(drink);
                    for (int i = deleteIndex; i < _drinksService.Drinks.Count; i++)
                    {
                        _drinksService.Drinks[i].Number = i + 1; 
                    }
                }
            }
        }
        private async void OnEditDrink(Drink drink)
        {
            var editDrinkVm = new EditDrinkPageViewModel(drink, _drinksService);
            var editDrinkPage = new EditDrinkPage(editDrinkVm); 
            await Shell.Current.Navigation.PushAsync(editDrinkPage);
        }
        private void OnSaveLayout()
        {
            // Logic for saving layout
        }

        private void OnLoadLayout()
        {
            // Logic for loading layout
        }

        private void OnDeleteLayout()
        {
            // Logic for deleting layout
        }
        private async void NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
