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

    [RelayCommand]
    private void New() => OpenEditWindow(new Customer(), isNew: true);

    [RelayCommand]
    private void Edit(Customer c) => OpenEditWindow(c, isNew: false);

    [RelayCommand]
    private async Task Delete(Customer c)
    {
        var mainPage = Application.Current!.Windows[0].Page;
        if (await mainPage.DisplayAlert("Excluir", $"Excluir {c.Name} {c.Lastname}?", "Sim", "Não"))
            _repo.Remove(c);
    }

    private void OpenEditWindow(Customer customer, bool isNew)
    {
        var vm = _sp.GetRequiredService<CustomerEditViewModel>();
        var page = _sp.GetRequiredService<CustomerEditPage>();

        vm.Initialize(customer, isNew, _repo);
        page.BindingContext = vm;

        var child = new Window(page)
        {
            Title = isNew ? "Novo Cliente" : "Editar Cliente",
            Width = 600,
            Height = 420
        };

#if WINDOWS
        child.Created += (_, __) =>
        {
            var info = Microsoft.Maui.Devices.DeviceDisplay.Current.MainDisplayInfo;
            var w = info.Width / info.Density;
            var h = info.Height / info.Density;
            child.X = (w - child.Width) / 2;
            child.Y = (h - child.Height) / 2;
        };
#endif
        Application.Current!.OpenWindow(child);
    }
}
