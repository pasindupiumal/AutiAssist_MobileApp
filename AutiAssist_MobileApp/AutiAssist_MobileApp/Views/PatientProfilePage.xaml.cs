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
    public partial class PatientProfilePage : ContentPage
    {
        private PatientProfileViewModel patientProfileViewModel;
        public PatientProfilePage()
        {
            InitializeComponent();

            BindingContext = patientProfileViewModel = new PatientProfileViewModel();
        }

        protected async override void OnAppearing()
        {
            await patientProfileViewModel.GetProfileDataCommand.ExecuteAsync();
        }
    }
}