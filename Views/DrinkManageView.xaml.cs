using Beursfuif.Models;
namespace Beursfuif.Views
{
    public partial class DrinkManageView : ContentView
    {
        private Drink _drink;
        private DrinksViewModel _viewModel;

        public DrinkManageView(Drink drink, DrinksViewModel viewModel)
        {
            InitializeComponent();
            _drink = drink;
            _viewModel = viewModel;
            BindingContext = _drink;
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            // Navigate to an edit page or show an edit form
        }

        public event EventHandler RequestDelete;

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            // Invoke the event
            RequestDelete?.Invoke(this, new EventArgs());
        }

    }
}