using A_Connect.Views;

namespace A_Connect;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        //Register SetDisplayName pages.
        Routing.RegisterRoute(nameof(AccountDetailsPage), typeof(AccountDetailsPage));



        // Register existing pages.
        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
<<<<<<< HEAD
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

        // Register InternNJobs pages.
        Routing.RegisterRoute("InternNJobsNewsfeedPage", typeof(InternNJobsNewsfeedPage));
        Routing.RegisterRoute(nameof(InternNJobsFormPage), typeof(InternNJobsFormPage));
        Routing.RegisterRoute(nameof(InternNJobsIndivPage), typeof(InternNJobsIndivPage));


        // Register Marketplace Pages.
        Routing.RegisterRoute(nameof(MarketplaceNewsFeedPage), typeof(MarketplaceNewsFeedPage));
        Routing.RegisterRoute(nameof(MarketplaceFormPage), typeof(MarketplaceFormPage));
        Routing.RegisterRoute(nameof(MarketplaceListingDetailPage), typeof(MarketplaceListingDetailPage));

=======
        // Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
        Routing.RegisterRoute(nameof(ScheduleTradingPage), typeof(ScheduleTradingPage));
        Routing.RegisterRoute(nameof(ScheduleTradingFormPage), typeof(ScheduleTradingFormPage));

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Jump straight to ScheduleTradingPage
        //await Shell.Current.GoToAsync(nameof(ScheduleTradingPage));
>>>>>>> 1fd85c8a5cd5e51b8cec819bf599391bd6871904
    }
}
