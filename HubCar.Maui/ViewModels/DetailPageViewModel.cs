using CommunityToolkit.Mvvm.ComponentModel;
using HubCar.Shared.Models;

namespace HubCar.Maui.ViewModels
{
    public  partial class DetailPageViewModel : BaseViewModel
    {
        [ObservableProperty] private Car _car;

        public Task SetCar(Car car)
        {
            Car = car;
            return Task.CompletedTask;
        }
    }
}