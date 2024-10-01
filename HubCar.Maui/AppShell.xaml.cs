using HubCar.Maui.Pages;

namespace HubCar.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("detailpage", typeof(DetailPage));
    }
}