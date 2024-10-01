using HubCar.Shared.Models;

namespace HubCar.Maui.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        public Car? Car { get; set; }
        public Task SetCar(Car car)
        {
            Car = car;
            return Task.CompletedTask;
        }
    }
}