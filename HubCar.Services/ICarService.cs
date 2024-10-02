using HubCar.Shared.Models;

namespace HubCar.Services
{
    public interface ICarService
    {
        Task<List<Car>?> GetCarsAsync(FilterRequest filterRequest);
        Task<List<string?>> GetMakeAsync();
        Task<List<string?>> GetModelsAsync(string? make);
    }
}