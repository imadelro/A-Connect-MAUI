﻿using A_Connect.Services;
﻿using A_Connect.ViewModels;
using Microsoft.Extensions.Logging;
using SQLite;

namespace A_Connect;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        // register the UserDatabase.
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
        builder.Services.AddSingleton<UserDatabase>(s => new UserDatabase(dbPath));

        // Register ScheduleTradingService with its own database
        string scheddbPath = Path.Combine(FileSystem.AppDataDirectory, "scheduleTrading.db3");
        var connection = new SQLiteAsyncConnection(scheddbPath);
        builder.Services.AddSingleton(new ScheduleTradingService(connection));

				    builder.Services.AddSingleton<TutorFinderDatabase>(s =>
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tutorfinder.db");
        return new TutorFinderDatabase(dbPath);
    });
        builder.Services.AddTransient<MarketplaceListingDetailViewModel>();
        builder.Services.AddSingleton<MarketplaceDatabase>(s =>
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "marketplace.db3");
            return new MarketplaceDatabase(dbPath);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
