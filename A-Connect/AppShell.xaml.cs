using A_Connect.Views;

namespace A_Connect;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register pages for navigation
        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
    }
}
