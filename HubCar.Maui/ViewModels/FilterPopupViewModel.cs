using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HubCar.Services;
using HubCar.Shared.Models;

namespace HubCar.Maui.ViewModels
{
    public partial class FilterPopupViewModel : BaseViewModel
    {
        private readonly CarService _carService;

        [ObservableProperty] private FilterRequest _filterRequest;
        [ObservableProperty] private string _title = "Filter";
        [ObservableProperty] private ObservableCollection<string?> _modelItems = new();
        [ObservableProperty] private ObservableCollection<string?> _makeItems = new();
        [ObservableProperty] private bool _isModelEnabled;
        [ObservableProperty] private bool _isFavorite;
        [ObservableProperty] private string _selectedModel;
        [ObservableProperty] private string _selectedMake;
        [ObservableProperty] private double _minimumBid;
        [ObservableProperty] private double _maximumBid;
        [ObservableProperty] private FilterSort _selectedFilterSort = FilterSort.MakeAscending;

        public IReadOnlyCollection<FilterSort> FilterSortItems  => FilterSort.List;

        [RelayCommand]
        async Task MakeSelected(string selectedMake)
        {
            if (!string.IsNullOrEmpty(selectedMake))
            {
                IsModelEnabled = true;
                await LoadModels(selectedMake);
                await LoadMakers(selectedMake);
            }
            else
            {
                IsModelEnabled = false;
            }
        }

        public FilterPopupViewModel(CarService carService)
        {
            _carService = carService; 
            _ = LoadMakers();
        }

        private async Task LoadMakers(string? make = null)
        {
            var makeItems = await _carService.GetMakeAsync();

            if (make is not null)
                makeItems = makeItems.OrderBy(brand => brand != make)
                    .ThenBy(brand => brand).ToList();
            
            MakeItems.Clear();
            
            foreach (var makeItem in makeItems)
            {
                MakeItems?.Add(makeItem);
            }
            
        }

        public async Task LoadModels(string make)
        {
            var modelItems = await _carService.GetModelsAsync(make);
            
            ModelItems.Clear();
            foreach (var modelItem in modelItems)
            {
                ModelItems?.Add(modelItem);
            }
        }
        
        
    }
}