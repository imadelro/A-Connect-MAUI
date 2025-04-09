using A_Connect.Models;
using A_Connect.Services;
using A_Connect.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace A_Connect.Views
{
    public partial class InternNJobsFormPage : ContentPage
    {

        public InternNJobsFormPage()
        {
            InitializeComponent();
            BindingContext = new InternNJobsFormViewModel();
        }

    }
}
