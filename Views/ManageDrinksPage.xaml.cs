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
                    var drinkView = new DrinkManageView(drink, _viewModel); // Ensure DrinkManageView is implemented
                    layout.Children.Add(drinkView);
                }
            }
            else
            {
                // Handle the case where the layout is not found
                // You might want to log this situation or throw a more informative exception
                throw new InvalidOperationException("DrinksLayout not found on ManageDrinksPage.");
            }
        }

        // Add methods for adding, editing, and deleting drinks
    }
}