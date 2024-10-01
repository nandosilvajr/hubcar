using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using HubCar.Maui.ViewModels;
using HubCar.Shared.Models;

namespace HubCar.Maui.Pages
{
    public partial class FilterPopup
    {
        
        public FilterPopup(FilterPopupViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
        async void OnOkButtonClicked(object? sender, EventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            
            if (BindingContext is FilterPopupViewModel bindingContext)
            {

                bindingContext?.LoadModels(bindingContext.SelectedMake);
                
                var filterRequest = new FilterRequest();

                if(bindingContext?.SelectedFilterSort is not null)
                    filterRequest.FilterSort = bindingContext?.SelectedFilterSort?.Name ?? string.Empty;
                filterRequest.Make = bindingContext?.SelectedMake ?? string.Empty;
                filterRequest.Model = bindingContext?.SelectedModel ?? string.Empty;
                filterRequest.MimimumBid = bindingContext?.MinimumBid ?? 0;
                filterRequest.MaximiumBid = bindingContext?.MaximumBid ?? 0;
                filterRequest.IsFavorite = bindingContext?.IsFavorite ?? false;
                
                await CloseAsync(filterRequest, cts.Token);
            }
        }

        async void OnNoButtonClicked(object? sender, EventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await CloseAsync(false, cts.Token);
        }

        private void MakeOnSelectedIndexChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                if (BindingContext is FilterPopupViewModel bindingContext)
                {
                    bindingContext.IsModelEnabled = true;
                    bindingContext?.LoadModels(bindingContext.SelectedMake);
                }
            }
            else
            {
                if (BindingContext is FilterPopupViewModel bindingContext) 
                    bindingContext.IsModelEnabled = false;
            }
        }
    }
}