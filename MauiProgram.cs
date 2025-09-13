using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BtgClients.Services;
using BtgClients.ViewModels;
using BtgClients.Views;

namespace BtgClients;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit();

        // Services / Repos (Persistência de dados)
        builder.Services.AddSingleton<ICustomerRepository, JsonCustomerRepository>();

        // VMs
        builder.Services.AddSingleton<CustomersViewModel>();
        builder.Services.AddTransient<CustomerEditViewModel>();

        // Views
        builder.Services.AddSingleton<CustomersPage>();
        builder.Services.AddTransient<CustomerEditPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
