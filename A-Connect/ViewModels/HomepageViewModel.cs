using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using A_Connect.Views;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class HomepageViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToNavBar { get; }

        public HomepageViewModel()
        {
            NavigateToHomeCommand = new Command(async () => await NavigateToHomeAsync());
            NavigateToNavBar = new Command(async () => await NavigateToNavBarAsync());
        }

        private async Task NavigateToHomeAsync()
        {
    await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task NavigateToNavBarAsync()
        {
            // /working
            await Shell.Current.GoToAsync("TutorFinderPage");
            
        }
    }
}