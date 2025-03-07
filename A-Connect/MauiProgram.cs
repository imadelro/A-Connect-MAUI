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
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
        builder.Services.AddSingleton<UserDatabase>(s => new UserDatabase(dbPath));
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
