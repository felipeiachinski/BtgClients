using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BtgClients.Models;
using BtgClients.Services;

namespace BtgClients.ViewModels;

public partial class CustomerEditViewModel : ObservableObject
{
    private ICustomerRepository _repo = default!;
    private bool _isNew;

    [ObservableProperty] private Customer model = new();
    [ObservableProperty] private string ageText = "";

    public event Action? CloseRequested;

    public void Initialize(Customer model, bool isNew, ICustomerRepository repo)
    {
        _repo = repo;
        _isNew = isNew;
        Model = model;
        AgeText = isNew ? string.Empty : (Model.Age > 0 ? Model.Age.ToString() : string.Empty);
    }

    [RelayCommand]
    private void Cancel() => CloseRequested?.Invoke();

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Model.Name))
        {
            await OnInvalidAsync("Entre com o nome do cliente."); return;
        }
        if (string.IsNullOrWhiteSpace(Model.Lastname))
        {
            await OnInvalidAsync("Entre com o sobrenome do cliente."); return;
        }
        if (!int.TryParse(AgeText, out var age) || age < 0)
        {
            await OnInvalidAsync("Entre com uma idade válida para o cliente."); return;
        }
        if (string.IsNullOrWhiteSpace(Model.Address))
        {
            await OnInvalidAsync("Entre com o endereço do cliente."); return;
        }

        Model.Age = age;
        if (_isNew) _repo.Add(Model); else _repo.Update(Model);

        CloseRequested?.Invoke();
    }

    public event Func<string, Task>? InvalidRequested;
    private Task OnInvalidAsync(string msg) => InvalidRequested?.Invoke(msg) ?? Task.CompletedTask;
}

