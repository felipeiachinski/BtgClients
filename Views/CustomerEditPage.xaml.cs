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
            await Dispatcher.DispatchAsync(async () =>
            {
                // Se foi empurrada na pilha normal
                if (Navigation?.NavigationStack?.Contains(this) == true)
                {
                    await Navigation.PopAsync();
                    return;
                }

                // Se foi apresentada como modal
                if (Navigation?.ModalStack?.Contains(this) == true)
                {
                    await Navigation.PopModalAsync();
                    return;
                }

                // Fallback: se realmente estiver em outra Window
                if (Window is not null)
                {
                    Application.Current?.CloseWindow(Window);
                }
            });
        };
    }
}

