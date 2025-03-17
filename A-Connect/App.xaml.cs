using System;
using System.IO;
using A_Connect.Services;
using A_Connect.Models;
using Microsoft.Maui.Controls;

namespace A_Connect;

public partial class App : Application
{
    // Store the logged-in user
    public static User CurrentUser { get; set; }

    // Initialize ReviewDatabase
    private static ReviewDatabase _reviewDatabase;
    public static ReviewDatabase ReviewDatabase
    {
        get
        {
            if (_reviewDatabase == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reviews.db3");
                _reviewDatabase = new ReviewDatabase(dbPath);
            }
            return _reviewDatabase;
        }
    }

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
