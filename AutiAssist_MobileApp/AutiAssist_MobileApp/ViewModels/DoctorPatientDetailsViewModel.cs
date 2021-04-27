using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using MvvmHelpers.Commands;
using AutiAssist_MobileApp.Views;
using System.Threading.Tasks;

namespace AutiAssist_MobileApp.ViewModels
{
    [QueryProperty(nameof(UserObject), nameof(UserObject))]
    public class DoctorPatientDetailsViewModel : BaseViewModel
    {
        private string userObject;
        private User patient;

        public AsyncCommand GoToReportsCommand { get; }

        public DoctorPatientDetailsViewModel()
        {
            Title = "Patient Details";
            GoToReportsCommand = new AsyncCommand(NavigateToReports);
        }

        public string UserObject
        {
            get => userObject;
            set
            {
                userObject = value;
                LoadPatientData(value);
            }
        }

        public User Patient
        {
            get => patient;
            set => SetProperty(ref patient, value);
        }

        public void LoadPatientData(string userObject)
        {
            try
            {
                Patient = JsonConvert.DeserializeObject<User>(userObject);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Patient");
            }
        }

        private async Task NavigateToReports()
        {
            await Shell.Current.GoToAsync($"{nameof(DoctorPatientReportsPage)}");
        }
    }
}
