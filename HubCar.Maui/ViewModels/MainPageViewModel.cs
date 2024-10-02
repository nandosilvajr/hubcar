using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HubCar.Maui.Pages;
using HubCar.Shared.Models;
using CommunityToolkit.Maui.Views;
using HubCar.Services;

namespace HubCar.Maui.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CarService _carService;

        [ObservableProperty] private ObservableCollection<Car>? _carItems = new ();
        [ObservableProperty] private bool _isNextPageEnable;
        [ObservableProperty] private FilterRequest _filterRequest = new ();
        [ObservableProperty] private Car? _selectedCar;

        [RelayCommand]
        async Task NextPage()
        {
            IsBusy = true;
            FilterRequest.PageNumber++;
            await LoadData(FilterRequest);
            IsBusy = false;
            OnPropertyChanged(nameof(FilterRequest));
        }
        
        [RelayCommand]
        async Task PreviousPage()
        {
            IsBusy = true;
            if(FilterRequest.PageNumber > 1)
                FilterRequest.PageNumber--;
            await LoadData(FilterRequest);
            IsBusy = false;
            OnPropertyChanged(nameof(FilterRequest));
        }

        [RelayCommand]
        async Task OpenFilters()
        {
            var popupViewModel = _serviceProvider.GetRequiredService<FilterPopupViewModel>();
            popupViewModel.FilterRequest = FilterRequest;
            var popup = new FilterPopup(popupViewModel);
            var result = await Shell.Current.ShowPopupAsync(popup);
            
            if (result is FilterRequest filterResult)
            {
                FilterRequest = filterResult;
                await LoadData(FilterRequest);
            }
        }

        [RelayCommand]
        async Task SelectedCarExecute(Car? car)
        {
            if (car is not null)
            {
                var navigationParameter = new ShellNavigationQueryParameters
                {
                    { "Car", car }
                };
                await Shell.Current.GoToAsync($"detailpage", navigationParameter); 
            }
        }

        public MainPageViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _carService = _serviceProvider.GetRequiredService<CarService>();
        }

        public async Task LoadData(FilterRequest filterRequest)
        {
            var cars = await _carService.GetCarsAsync(filterRequest) ?? new List<Car>();

            CarItems?.Clear();
            
            foreach (var car in cars)
            {
                CarItems?.Add(car);
            }
            
            IsNextPageEnable = cars.Count == filterRequest.PageSize;
        }
    }
}