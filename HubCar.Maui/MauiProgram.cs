using CommunityToolkit.Maui;
using HubCar.Maui.Pages;
using HubCar.Maui.Services;
using HubCar.Maui.ViewModels;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;
using Xceed.Maui.Toolkit;

namespace HubCar.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMaterialMauiIcons()
            .UseXceedMauiToolkit(FluentDesignAccentColor.DarkLily)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIconsRound-Regular.otf", "MaterialIcon");
            });
        builder.Services.AddSingleton<CarService>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<DetailPageViewModel>();
        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<FilterPopup, FilterPopupViewModel>();
        
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
        
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
        
        Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.ContentInsetStartWithNavigation = 0;
            handler.PlatformView.SetContentInsetsAbsolute(0, 0);
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}