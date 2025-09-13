using System.Collections.ObjectModel;
using System.Text.Json;
using BtgClients.Models;

namespace BtgClients.Services;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _path = Path.Combine(FileSystem.AppDataDirectory, "customers.json");

    public ObservableCollection<Customer> Items { get; } = new();

    public async Task LoadAsync()
    {
        if (!File.Exists(_path)) return;
        var json = await File.ReadAllTextAsync(_path);
        var data = JsonSerializer.Deserialize<List<Customer>>(json) ?? new();
        Items.Clear();
        foreach (var c in data) Items.Add(c);
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(Items);
        await File.WriteAllTextAsync(_path, json);
    }

    public void Add(Customer c)
    {
        Items.Add(c);
        _ = SaveAsync();
    }

    public void Update(Customer c)
    {
        _ = SaveAsync();
    }

    public void Remove(Customer c)
    {
        Items.Remove(c);
        _ = SaveAsync();
    }
}
