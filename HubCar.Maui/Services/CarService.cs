using System.Text.Json;
using HubCar.Maui.Converters;
using HubCar.Shared.Models;

namespace HubCar.Maui.Services
{
    public class CarService
    {
        private readonly string _jsonPath = "Data/vehicles_dataset.json";
        
        public async Task<List<Car>?> GetCarsAsync(FilterRequest filterRequest)
        {
            try
            {
                await using var stream = await FileSystem.OpenAppPackageFileAsync(_jsonPath);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                
                var options = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new CustomDateTimeConverter()
                    },
                };
                
                var cars = JsonSerializer.Deserialize<List<Car>>(json, options);

                if (!string.IsNullOrEmpty(filterRequest.Make))
                    cars = cars?.Where(x => x.Make?.Equals(filterRequest.Make) is true).ToList();
                
                if (!string.IsNullOrEmpty(filterRequest.Model))
                    cars = cars?.Where(x => x.Model?.Equals(filterRequest.Model) is true).ToList();
                
                if (filterRequest.IsFavorite)
                    cars = cars?.Where(x => x.Favourite).ToList();
                
                if (filterRequest.MimimumBid > 0)
                    cars = cars?.Where(x => x.StartingBid > filterRequest.MimimumBid).ToList();
                
                if (filterRequest.MaximiumBid > 0 && filterRequest.MaximiumBid > filterRequest.MimimumBid )
                    cars = cars?.Where(x => x.StartingBid > filterRequest.MimimumBid).ToList();
                
                if (!string.IsNullOrEmpty(filterRequest.FilterSort))
                {
                    var filterSort = GetSortProperty(filterRequest.FilterSort);
                    
                    if(filterSort.Key is not null && filterSort.Value is not null)
                    {
                        if (filterSort.Value.Equals("Asc"))
                            cars = cars?.OrderBy(car => car.GetType().GetProperty(filterSort.Key)?.GetValue(car, null))
                                .ToList();
                        else
                            cars = cars?.OrderByDescending(car => car.GetType().GetProperty(filterSort.Key)?.GetValue(car, null))
                                .ToList();
                    }
                }
                
                return cars?
                    .Skip((filterRequest.PageNumber - 1) * filterRequest.PageSize)
                    .Take(filterRequest.PageSize)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<string?>> GetMakeAsync()
        {
            try
            {
                await using var stream = await FileSystem.OpenAppPackageFileAsync(_jsonPath);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                
                var options = new JsonSerializerOptions
                {
                    Converters = { new CustomDateTimeConverter() },
                };
                
                var cars = JsonSerializer.Deserialize<List<Car>>(json, options);

                if (cars?.Any() is true)
                {
                    
                    var makers = cars.Select(car => car.Make).Distinct().ToList();
                    if (makers.Any())
                        return makers;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new List<string?>();
        }

        private KeyValuePair<string?, string?> GetSortProperty(string sort )
        {
            var list = FilterSort.List;

            var sortProperty = list.Where(x => x.Name.Equals(sort)).Select(x => new
            {
                x.Sort,
                x.Property
            }).FirstOrDefault();
            return new KeyValuePair<string?, string?>(sortProperty?.Property, sortProperty?.Sort);
        }
        
        public async Task<List<string?>> GetModelsAsync(string? make)
        {
            try
            {
                await using var stream = await FileSystem.OpenAppPackageFileAsync(_jsonPath);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                
                var options = new JsonSerializerOptions
                {
                    Converters = { new CustomDateTimeConverter() },
                };
                
                var cars = JsonSerializer.Deserialize<List<Car>>(json, options);

                if (cars?.Any() is true)
                {
                    var models = cars
                        .Where(x => x.Make?.Equals(make) is true)
                        .Select(car => car.Model)
                        .Distinct()
                        .ToList();
                    if (models.Any())
                        return models;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new List<string?>();
        }
    }
}