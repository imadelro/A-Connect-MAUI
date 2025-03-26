﻿using A_Connect.Views;

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
        Routing.RegisterRoute("TutorFinderPage", typeof(TutorFinderPage));
        Routing.RegisterRoute("ScheduleTradingNewsFeedPage", typeof(ScheduleTradingNewsFeedPage));
        Routing.RegisterRoute("IndividualPostPage", typeof(IndividualPostPage));
        Routing.RegisterRoute("PostTradePage", typeof(PostTradePage));

        Routing.RegisterRoute("ProfsToPickNewsFeedPage", typeof(ProfsToPickNewsFeedPage));
        Routing.RegisterRoute("ProfsToPickFormPage", typeof(ProfsToPickFormPage));
        Routing.RegisterRoute(nameof(TutorFinderPage), typeof(TutorFinderPage));
        Routing.RegisterRoute(nameof(TutorFinderFormPage), typeof(TutorFinderFormPage));
        Routing.RegisterRoute(nameof(TutorFinderDetailsPage), typeof(TutorFinderDetailsPage));

        // Register Marketplace Pages.
        Routing.RegisterRoute("MarketplaceFormPage", typeof(MarketplaceFormPage));
    }
}
