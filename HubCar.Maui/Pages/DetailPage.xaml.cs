using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubCar.Maui.ViewModels;
using HubCar.Shared.Models;

namespace HubCar.Maui.Pages
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class DetailPage : ContentPage
    {
        public Car Car
        { get; set; }
        public DetailPage(DetailPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var vm = (DetailPageViewModel)BindingContext;
            await vm.SetCar(Car);
            base.OnAppearing();
        }
    }
}