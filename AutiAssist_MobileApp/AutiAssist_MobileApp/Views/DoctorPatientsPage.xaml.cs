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
    public partial class DoctorPatientsPage : ContentPage
    {
        private DoctorPatientsViewModel doctorPatientViewModel;
        public DoctorPatientsPage()
        {
            InitializeComponent();
            BindingContext = doctorPatientViewModel = new DoctorPatientsViewModel();
        }

        protected async override void OnAppearing()
        {
            await doctorPatientViewModel.GetPatientsCommand.ExecuteAsync();
        }

        protected override void OnDisappearing()
        {
            doctorPatientViewModel.ClearList();
        }
    }
}