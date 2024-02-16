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
            // Clear the layout
            var layout = this.FindByName<VerticalStackLayout>("DrinksLayout");

            // Dynamically add drinks to the layout
            foreach (var drink in _viewModel.Drinks)
            {
                var drinkView = new DrinkManageView(drink, _viewModel); // You will create this custom view
                layout.Children.Add(drinkView);
            }
        }

        // Add methods for adding, editing, and deleting drinks
    }
}