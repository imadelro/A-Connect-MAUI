using A_Connect.Views;  // Add this

namespace A_Connect;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Fix: Remove nameof(LoginPage) and use string
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
    }
}
