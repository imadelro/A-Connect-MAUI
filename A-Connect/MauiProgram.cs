using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using A_Connect;

namespace A_Connect
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

            // Register the database service
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

            return builder.Build();
        }
    }
}
