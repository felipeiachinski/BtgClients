using System.Collections.ObjectModel;
using BtgClients.Models;

namespace BtgClients.Services;

public class InMemoryCustomerRepository : ICustomerRepository
{
    public ObservableCollection<Customer> Items { get; } = new();

    public void Add(Customer c) => Items.Add(c);
    public void Update(Customer c) { /* Atualizações via binding */ }
    public void Remove(Customer c) => Items.Remove(c);

    public Task LoadAsync() => Task.CompletedTask;
    public Task SaveAsync() => Task.CompletedTask;
}
