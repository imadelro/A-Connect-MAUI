namespace A_Connect;

public partial class App : Application
{
    // Add this property to store the logged in user.
    public static A_Connect.Models.User CurrentUser { get; set; }

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
