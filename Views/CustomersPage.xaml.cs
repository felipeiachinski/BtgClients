using BtgClients.ViewModels;

namespace BtgClients.Views;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
