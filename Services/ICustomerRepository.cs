using System.Collections.ObjectModel;
using BtgClients.Models;

namespace BtgClients.Services;

public interface ICustomerRepository
{
    ObservableCollection<Customer> Items { get; }
    void Add(Customer c);
    void Update(Customer c);
    void Remove(Customer c);
    Task LoadAsync();
    Task SaveAsync();
}
