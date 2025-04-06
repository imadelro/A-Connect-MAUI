using A_Connect.Services;
using Microsoft.Extensions.Logging;

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
        string userDbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
        builder.Services.AddSingleton<UserDatabase>(s => new UserDatabase(userDbPath));
        // register the tutorFinder database
        string tutorFinderDbPath = Path.Combine(FileSystem.AppDataDirectory, "tutorFinder.db3");
        builder.Services.AddSingleton<TutorFinderDatabase>(s => new TutorFinderDatabase(tutorFinderDbPath));
        // Register marketplace
        string marketplaceDbPath = Path.Combine(FileSystem.AppDataDirectory, "marketplace.db3");
        builder.Services.AddSingleton<MarketplaceDatabase>(s => new MarketplaceDatabase(marketplaceDbPath));


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
