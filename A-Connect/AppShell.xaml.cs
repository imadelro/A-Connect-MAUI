using A_Connect.Views;

namespace A_Connect;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register existing pages.
        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));

        // Register Schedule Trading pages.
        Routing.RegisterRoute("ScheduleTradingNewsFeedPage", typeof(ScheduleTradingNewsFeedPage));
        Routing.RegisterRoute("IndividualPostPage", typeof(IndividualPostPage));
        Routing.RegisterRoute("OwnPostsPage", typeof(OwnPostsPage));
        Routing.RegisterRoute("PostTradePage", typeof(PostTradePage));
    }
}
