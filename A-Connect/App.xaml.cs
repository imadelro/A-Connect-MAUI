using System;
using System.IO;
using A_Connect.Services;
using A_Connect.Models;
using A_Connect.ViewModels;
using Microsoft.Maui.Controls;

namespace A_Connect;

public partial class App : Application
{
    // Store the logged-in user

    public static bool IsOwnPostsSelected { get; set; }

    public static UserDatabase userDb { get; set; }

    public static A_Connect.Models.User CurrentUser { get; set; }
    public static A_Connect.Services.STFormDatabase STDB { get; private set; }
    public static MarketplaceDatabase MarketplaceDB { get; private set; }

    public static OpportunityDatabase OpportunityDatabase { get; private set; }

    // Initialize ReviewDatabase
    private static ReviewDatabase _reviewDatabase;
    public static ReviewDatabase ReviewDatabase
    {
        get
        {
            if (_reviewDatabase == null)
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "reviews.db3");
                _reviewDatabase = new ReviewDatabase(dbPath);
            }
            return _reviewDatabase;
        }
    }

    public App()
{
    InitializeComponent();

    // Paths
    string userDbPath = Path.Combine(FileSystem.AppDataDirectory, "User.db3");
    string opportunityDbPath = Path.Combine(FileSystem.AppDataDirectory, "opportunities.db3");
    string STdbPath = Path.Combine(FileSystem.AppDataDirectory, "STForms.db3");
    string marketplaceDbPath = Path.Combine(FileSystem.AppDataDirectory, "marketplace.db3");

        // Init & Register Databases
    userDb = new UserDatabase(userDbPath);
    DependencyService.RegisterSingleton<UserDatabase>(userDb);


        OpportunityDatabase = new OpportunityDatabase(opportunityDbPath);
    DependencyService.RegisterSingleton<OpportunityDatabase>(OpportunityDatabase);

    // Other DBs
    STDB = new STFormDatabase(STdbPath);
    MarketplaceDB = new MarketplaceDatabase(marketplaceDbPath);
    DependencyService.Register<MarketplaceNewsFeedViewModel>();


        DependencyService.Register<InternNJobsNewsfeedViewModel>();
}


    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
