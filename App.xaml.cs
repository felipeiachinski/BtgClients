using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.LifecycleEvents;
using BtgClients.Views;
#if WINDOWS
using Microsoft.UI.Windowing;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Devices;
#endif

namespace BtgClients;

public partial class App : Application
{
    private readonly CustomersPage _rootPage;

    public App(CustomersPage rootPage)
    {
        InitializeComponent();
        _rootPage = rootPage;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_rootPage)
        {
            Title = "BTG Clients"
        };

#if WINDOWS
        window.Created += (_, __) =>
        {
            var winui = (MauiWinUIWindow)window.Handler.PlatformView;
            var appW = winui.AppWindow;
            if (appW.Presenter is OverlappedPresenter p)
                p.Maximize(); // abre maximizado
        };
#endif
        return window;
    }
}
