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
        // Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
        Routing.RegisterRoute(nameof(ScheduleTradingPage), typeof(ScheduleTradingPage));
        Routing.RegisterRoute(nameof(ScheduleTradingFormPage), typeof(ScheduleTradingFormPage));

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Jump straight to ScheduleTradingPage
        //await Shell.Current.GoToAsync(nameof(ScheduleTradingPage));
    }
}
