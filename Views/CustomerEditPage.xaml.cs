using BtgClients.ViewModels;

namespace BtgClients.Views;

public partial class CustomerEditPage : ContentPage
{
    public CustomerEditPage(CustomerEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        vm.CloseRequested += () =>
        {
            var w = this.Window;
            if (w is not null)
                Application.Current!.CloseWindow(w);
        };
    }
}
