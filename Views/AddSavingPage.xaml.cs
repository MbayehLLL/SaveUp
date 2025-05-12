using SaveUp.ViewModels;

namespace SaveUp.Views;

public partial class AddSavingPage : ContentPage
{ public AddSavingPage(AddSavingViewModel viewModel)
    { InitializeComponent(); BindingContext = viewModel; }
}