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
        public event EventHandler<Drink> EditRequested;
        private void OnEditClicked(object sender, EventArgs e)
        {
            EditRequested?.Invoke(this, _drink);
        }
        public event EventHandler RequestDelete;

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            RequestDelete?.Invoke(this, new EventArgs());
        }
    }
}