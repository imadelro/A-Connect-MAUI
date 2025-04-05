namespace A_Connect.Views;
using System;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;
public partial class InternNJobsFormPage : ContentPage
{
	public InternNJobsFormPage()
	{
		InitializeComponent();
	}

	private void OnTypePickerSelected(object sender, EventArgs e)
	{
			var picker = (Picker)sender;
			string selectedType = (string)picker.SelectedItem;  // Cast to string

			if (selectedType != null)
			{
					// Do something with the selected value
					DisplayAlert("Selected", $"You chose: {selectedType}", "OK");
			}
	}
}