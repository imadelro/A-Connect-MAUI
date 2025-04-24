using A_Connect.Models;
using A_Connect.Services;
using A_Connect.ViewModels;

namespace A_Connect.Views;

[QueryProperty(nameof(PostId), "PostId")]
public partial class InternNJobsIndivPage : ContentPage
{
    private readonly InternNJobsIndivViewModel _viewModel;

    public string PostId { get; set; }

    public InternNJobsIndivPage()
    {
        InitializeComponent();
        _viewModel = new InternNJobsIndivViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (int.TryParse(PostId, out int id))
        {
            await _viewModel.LoadPostAsync(id);
        }
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");  // Navigate back to the previous page
    }

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the HomePage
        await Shell.Current.GoToAsync("//HomePage");
    }

}