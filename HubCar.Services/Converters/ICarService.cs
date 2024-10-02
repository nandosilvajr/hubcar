using HubCar.Shared.Models;

namespace HubCar.Services.Converters
{
    public interface ICarService
    {
        Task<List<Car>?> GetCarsAsync(FilterRequest filterRequest);
        Task<List<string?>> GetMakeAsync();
        Task<List<string?>> GetModelsAsync(string? make);
    }
}