using Microsoft.Maui.Controls;
using A_Connect.ViewModels;
using System.Collections.Generic;

namespace A_Connect.Views
{
    public partial class IndividualPostPage : ContentPage, IQueryAttributable
    {
        public IndividualPostPage()
        {
            InitializeComponent();
            BindingContext = new IndividualPostViewModel();
        }

        // This method is called automatically by Shell navigation.
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (BindingContext is IndividualPostViewModel vm)
            {
                vm.ApplyQueryAttributes(query);
            }
        }
    }
}
