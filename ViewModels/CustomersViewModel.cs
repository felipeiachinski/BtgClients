using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BtgClients.Models;
using BtgClients.Services;
using BtgClients.Views;

namespace BtgClients.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerRepository _repo;
    private readonly IServiceProvider _sp;

    public ObservableCollection<Customer> Customers => _repo.Items;

    [ObservableProperty] private Customer? selected;

    public CustomersViewModel(ICustomerRepository repo, IServiceProvider sp)
    {
        _repo = repo;
        _sp = sp;
    }

    [RelayCommand] private void New() => OpenEditWindow(null, true);
    [RelayCommand] private void Edit(Customer c) => OpenEditWindow(c, false);

    [RelayCommand]
    private async Task Delete(Customer c)
    {
        var mainPage = Application.Current!.Windows[0].Page;
        if (await mainPage.DisplayAlert("Excluir", $"Excluir {c.Name} {c.Lastname}?", "Sim", "Não"))
            _repo.Remove(c);
    }

    private void OpenEditWindow(Customer? customer, bool isNew)
    {
        var page = _sp.GetRequiredService<CustomerEditPage>();
        var vm = (CustomerEditViewModel)page.BindingContext;

        vm.Initialize(customer ?? new Customer(), isNew, _repo);

        WireAlerts(page, vm);

        Application.Current!.OpenWindow(new Window(page));
    }

    private static void WireAlerts(CustomerEditPage page, CustomerEditViewModel vm)
    {
        Func<string, Task> handler = async (msg) =>
        {
            await page.DisplayAlert("Atenção!", msg, "OK");
        };

        vm.InvalidRequested += handler;

        page.Disappearing += (_, __) => vm.InvalidRequested -= handler;
    }
}
