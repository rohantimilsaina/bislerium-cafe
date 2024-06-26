﻿using bislerium_cafe_pos.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace bislerium_cafe_pos
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.VisibleStateDuration = 4000;
                config.SnackbarConfiguration.HideTransitionDuration = 200;
                config.SnackbarConfiguration.ShowTransitionDuration = 200;
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 6;
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomStart;
            });

            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<CoffeeServices>();
            builder.Services.AddSingleton<AddInItemsServices>();
            builder.Services.AddSingleton<OrderItemServices>();
            builder.Services.AddSingleton<OrderServices>();
            builder.Services.AddSingleton<CustomerServices>();
            builder.Services.AddSingleton<ReportDataService>();

            return builder.Build();
        }
    }
}
