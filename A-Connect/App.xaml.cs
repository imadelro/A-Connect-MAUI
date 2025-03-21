namespace A_Connect;
using A_Connect.Services;

public partial class App : Application
{
    // Add this property to store the logged in user.
    public static A_Connect.Models.User CurrentUser { get; set; }
    public static A_Connect.Services.STFormDatabase STDB { get; private set; }
    public App()
    {
        InitializeComponent();
        string STdbPath = Path.Combine(FileSystem.AppDataDirectory, "STForms.db3");
    // Initialize the database
        STDB = new STFormDatabase(STdbPath);
    }
    

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
