using BtgClients.ViewModels;

namespace BtgClients.Views;

public partial class CustomerEditPage : ContentPage
{
    public CustomerEditPage(CustomerEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        vm.CloseRequested += async () =>
        {
            if (Navigation?.NavigationStack?.Contains(this) == true)
            {
                await Navigation.PopAsync();
                return;
            }

            var w = this.Window;
            if (w is not null)
                Application.Current?.CloseWindow(w);
        };
    }
}

