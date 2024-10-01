using HubCar.Maui.ViewModels;
using HubCar.Shared.Models;

namespace HubCar.Maui.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        var vm = (MainPageViewModel)BindingContext;
        await vm.LoadData(new FilterRequest());
        base.OnAppearing();
    }

    private async void OnPageSizeSelectedIndexChanged(object? sender, EventArgs e)
    {
        var vm = (MainPageViewModel)BindingContext;
        await vm.LoadData(vm.FilterRequest); 
    }
}