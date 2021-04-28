using AutiAssist_MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Xamarin.Essentials;
using AutiAssist_MobileApp.Services;
using Xamarin.Forms;
using System.Diagnostics;
using AutiAssist_MobileApp.Views;

namespace AutiAssist_MobileApp.ViewModels
{
    public class PatientProfileViewModel : BaseViewModel
    {
        private User patient;

        public AsyncCommand GetProfileDataCommand { get; }
        public AsyncCommand DoctorDetailsPageCommand { get; }

        public PatientProfileViewModel()
        {
            Title = "Profile";
            GetProfileDataCommand = new AsyncCommand(GetPatient);
            DoctorDetailsPageCommand = new AsyncCommand(DoctorDetailsPage);
        }

        private bool passwordSwitch;

        public bool PasswordSwitch
        {
            get => passwordSwitch;
            set { SetProperty(ref passwordSwitch, value); }
        }

        public User Patient
        {
            get => patient;
            set => SetProperty(ref patient, value);
        }

        private async Task GetPatient()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                //await Task.Delay(4000);

                var patientUsername = Preferences.Get("username", null);

                if (patientUsername != null)
                {
                    SingleUserResponse responseObject = await UserService.GetUserByUsername(patientUsername);

                    string match = "User retrieved for username - " + patientUsername;

                    if (responseObject.Message.Equals(match))
                    {
                        Patient = responseObject.Data;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", responseObject.Message, "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", "Preferences Error", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get patient profile data: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DoctorDetailsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(PatientDoctorDetailsPage)}");
        }
    }
}
