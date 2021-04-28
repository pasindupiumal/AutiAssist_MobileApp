using AutiAssist_MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutiAssist_MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorPatientReportsPage : ContentPage
    {
        private DoctorPatientReportsViewModel doctorPatientReportsViewModel;
        public DoctorPatientReportsPage()
        {
            InitializeComponent();

            BindingContext = doctorPatientReportsViewModel = new DoctorPatientReportsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await doctorPatientReportsViewModel.GetReportsCommand.ExecuteAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            doctorPatientReportsViewModel.ClearListCommand.Execute(null);

        }
    }
}