using CommunityToolkit.Mvvm.ComponentModel;

namespace BtgClients.Models;

public partial class Customer : ObservableObject
{
    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string lastname = string.Empty;
    [ObservableProperty] private int age;
    [ObservableProperty] private string address = string.Empty;
}
