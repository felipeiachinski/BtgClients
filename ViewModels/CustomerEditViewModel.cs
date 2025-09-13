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
        AgeText = isNew
            ? string.Empty
            : (Model.Age > 0 ? Model.Age.ToString() : string.Empty);
    }

    [RelayCommand]
    private void Cancel() => CloseRequested?.Invoke();

    [RelayCommand]
    private async Task Save()
    {
        var currentPage = Application.Current?.MainPage?.Navigation?
                                  .NavigationStack.LastOrDefault()
                      ?? Application.Current?.MainPage;

        if (string.IsNullOrWhiteSpace(Model.Name))
        {
            if (currentPage != null)
                await currentPage.DisplayAlert("Atenção!", "Entre com o nome do cliente.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Model.Lastname))
        {
            if (currentPage != null)
                await currentPage.DisplayAlert("Atenção!", "Entre com o sobrenome do cliente.", "OK");
            return;
        }

        if (!int.TryParse(AgeText, out var age) || age < 0)
        {
            if (currentPage != null)
                await currentPage.DisplayAlert("Atenção!", "Entre com uma idade válida para o cliente.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Model.Address))
        {
            if (currentPage != null)
                await currentPage.DisplayAlert("Atenção!", "Entre com o endereço do cliente.", "OK");
            return;
        }

        Model.Age = age;
        if (_isNew) _repo.Add(Model); else _repo.Update(Model);
        if (_repo is JsonCustomerRepository)
            await _repo.SaveAsync();

        CloseRequested?.Invoke();
    }
}
