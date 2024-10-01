using HubCar.Maui.Pages;

namespace HubCar.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(FilterPopup), typeof(FilterPopup));
    }
}