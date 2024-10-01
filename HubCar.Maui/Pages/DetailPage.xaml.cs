using HubCar.Maui.ViewModels;
using HubCar.Shared.Models;

namespace HubCar.Maui.Pages
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class DetailPage : ContentPage
    {
        public Car? Car { get; set; }
        
        public DetailPage(DetailPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var vm = (DetailPageViewModel)BindingContext;
            if(Car is not null)
                await vm.SetCar(Car);
            base.OnAppearing();
        }
    }
}