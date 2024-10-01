using CommunityToolkit.Mvvm.ComponentModel;

namespace HubCar.Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty] private bool isBusy;
    }
}