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

        private void OnSaveLayoutClicked(object sender, EventArgs e)
        {

        }
        private void OnLoadLayoutClicked(object sender, EventArgs e)
        {

        }
    }
    
}