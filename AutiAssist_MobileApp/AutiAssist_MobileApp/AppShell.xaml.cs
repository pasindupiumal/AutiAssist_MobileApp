using AutiAssist_MobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AutiAssist_MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(DoctorPatientDetailsPage), typeof(DoctorPatientDetailsPage));
            Routing.RegisterRoute(nameof(DoctorPatientReportsPage), typeof(DoctorPatientReportsPage));
            Routing.RegisterRoute(nameof(DoctorReportDetailsPage), typeof(DoctorReportDetailsPage));
            Routing.RegisterRoute(nameof(PatientDoctorDetailsPage), typeof(PatientDoctorDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            Preferences.Clear();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public void UpdateFlyoutMenuItems(string user)
        {
            if (user.ToLower().Equals("patient"))
            {
                FlyoutItem_PatientProfile.FlyoutItemIsVisible = true;
                FlyoutItem_DoctorProfile.FlyoutItemIsVisible = false;
                FlyoutItem_DoctorPatients.FlyoutItemIsVisible = false;
                FlyoutItem_PatientProfileReports.FlyoutItemIsVisible = true;
            }
            else if(user.ToLower().Equals("doctor"))
            {
                FlyoutItem_PatientProfile.FlyoutItemIsVisible = false;
                FlyoutItem_DoctorProfile.FlyoutItemIsVisible = true;
                FlyoutItem_DoctorPatients.FlyoutItemIsVisible = true;
                FlyoutItem_PatientProfileReports.FlyoutItemIsVisible = false;
            }
            else
            {
                return;
            }
            

        }
    }
}
