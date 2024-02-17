using Beursfuif.Models;
namespace Beursfuif.Views
{
    public partial class ManageDrinksPage : ContentPage
    {
        private DrinksViewModel _viewModel;

        public ManageDrinksPage(DrinksViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            LoadDrinks();
        }

        private void LoadDrinks()
        {
            // Find the VerticalStackLayout by name
            var layout = this.FindByName<VerticalStackLayout>("DrinksLayout");
            // Ensure that the layout is not null
            if (layout != null)
            {
                // Clear the layout
                layout.Children.Clear();

                // Dynamically add drinks to the layout
                foreach (var drink in _viewModel.Drinks)
                {
                    var drinkView = new DrinkManageView(drink, _viewModel);
                    drinkView.RequestDelete += DrinkView_RequestDelete;
                    drinkView.EditRequested += DrinkView_EditRequested; 
                    layout.Children.Add(drinkView);
                }
            }
            else
            {
                // Handle the case where the layout is not found
                throw new InvalidOperationException("DrinksLayout not found on ManageDrinksPage.");
            }
        }

        public event EventHandler DeleteRequested;

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
        private async void DrinkView_RequestDelete(object sender, EventArgs e)
        {
            if (sender is DrinkManageView drinkView)
            {
                var drink = drinkView.BindingContext as Drink;
                if (drink != null)
                {
                    bool isConfirmed = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this drink?", "Yes", "No");
                    if (isConfirmed)
                    {
                        _viewModel.DeleteDrink(drink);
                        LoadDrinks();
                    }
                }
            }
        }
        private async void DrinkView_EditRequested(object sender, Drink drink)
        {
            await Navigation.PushAsync(new EditDrinkPage(_viewModel, drink));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadDrinks(); 
        }

        private async void OnSaveLayoutClicked(object sender, EventArgs e)
        {
            var filename = await DisplayPromptAsync("Save Layout", "Enter a filename for the layout:");
            if (!string.IsNullOrWhiteSpace(filename))
            {
                var dataService = new DrinkDataService();
                bool result = await dataService.SaveDrinksLayoutAsync(_viewModel.Drinks, filename);
                if (result)
                {
                    await DisplayAlert("Success", "Layout saved successfully.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save the layout.", "OK");
                }
            }
        }

        private async void OnLoadLayoutClicked(object sender, EventArgs e)
        {
            var dataService = new DrinkDataService();
            var layouts = dataService.GetSavedLayouts().ToList();

            // Check if there are saved layouts
            if (layouts.Any())
            {
                // Converts the IEnumerable<string> to string[]
                var layoutChoices = layouts.ToArray();

                // DisplayActionSheet returns the name of the selected layout
                string layoutName = await DisplayActionSheet("Select a Layout", "Cancel", null, layoutChoices);

                // Ensure user didn't hit cancel and a layout was selected
                if (layoutName != null && layoutName != "Cancel")
                {
                    await _viewModel.LoadLayoutAsync(layoutName);
                }
            }
            else
            {
                await DisplayAlert("No Layouts Found", "There are no saved layouts to load.", "OK");
            }
        }
        private async void OnDeleteLayoutClicked(object sender, EventArgs e)
        {
            var dataService = new DrinkDataService();
            var layouts = dataService.GetSavedLayouts().ToList();

            // Prompt the user to select a layout to delete
            string layoutToDelete = await DisplayActionSheet("Select a layout to delete", "Cancel", null, layouts.ToArray());

            if (layoutToDelete != null && layoutToDelete != "Cancel")
            {
                bool isDeleted = await dataService.DeleteLayoutAsync(layoutToDelete);
                if (isDeleted)
                {
                    await DisplayAlert("Success", "Layout deleted successfully.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete the layout.", "OK");
                }
            }
        }


    }

}