using A_Connect.ViewModels;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class ProfsToPickFormPage : ContentPage
    {
        public ProfsToPickFormPage()
        {
            InitializeComponent();
            BindingContext = new ProfsToPickFormViewModel(App.ReviewDatabase);
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender is Slider slider)
            {
                // Snap the slider value to the nearest valid value (1, 2, 3, 4, 5)
                double snappedValue = Math.Round(slider.Value);

                // Prevent the slider from displaying 0
                if (snappedValue == 0)
                {
                    slider.Value = 0; // Keep the slider at 0 until moved
                }
                else
                {
                    slider.Value = snappedValue; // Update the slider's value to the snapped value
                }

                if (BindingContext is ProfsToPickFormViewModel viewModel)
                {
                    // Update the ViewModel with the snapped value
                    viewModel.SelectedRating = (int)snappedValue;
                }
            }
        }

    }
}
