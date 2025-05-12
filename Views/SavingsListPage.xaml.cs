using SaveUp.ViewModels;

namespace SaveUp.Views;

public partial class SavingsListPage : ContentPage 
{ public SavingsListPage(SavingsListViewModel viewModel) 
    { InitializeComponent(); BindingContext = viewModel; } 
}