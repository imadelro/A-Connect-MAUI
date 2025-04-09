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

    public static A_Connect.Models.User CurrentUser { get; set; }
    public static A_Connect.Services.STFormDatabase STDB { get; private set; }
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

        DependencyService.Register<OpportunityDatabase>();
        DependencyService.Register<InternNJobsNewsfeedViewModel>();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "opportunities.db3");
        OpportunityDatabase = new OpportunityDatabase(dbPath);

        DependencyService.RegisterSingleton<OpportunityDatabase>(OpportunityDatabase);


        string STdbPath = Path.Combine(FileSystem.AppDataDirectory, "STForms.db3");
    // Initialize the database
        STDB = new STFormDatabase(STdbPath);

    }
    

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
