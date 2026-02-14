using MauiApp2;
using MauiApp2.Models;
using MauiApp2.Services;
using MauiApp2.ViewModels;
using MauiApp2.Views;
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
