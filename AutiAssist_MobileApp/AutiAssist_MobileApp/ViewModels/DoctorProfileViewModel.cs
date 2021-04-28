using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class DoctorProfileViewModel : BaseViewModel
    {
        private User doctor;

        private bool passwordSwitch;

        public bool PasswordSwitch
        {
            get => passwordSwitch;
            set { SetProperty(ref passwordSwitch, value); OnPropertyChanged(nameof(PasswordSwitchVisibility)); }
        }

        public bool PasswordSwitchVisibility => !PasswordSwitch;

        public AsyncCommand GetProfileDataCommand { get; }
        public DoctorProfileViewModel()
        {
            Title = "Profile";
            GetProfileDataCommand = new AsyncCommand(GetDoctor);
        }

        public User Doctor
        {
            get => doctor;
            set => SetProperty(ref doctor, value);
        }

        private async Task GetDoctor()
        {
            if(IsBusy){
                return;
            }
            
            try
            {
                IsBusy = true;

                await Task.Delay(4000);

                var doctorUsername = Preferences.Get("username", null);

                if (doctorUsername != null)
                {
                    var responseObject = await UserService.GetUserByUsername(doctorUsername);

                    string match = "User retrieved for username - " + doctorUsername;

                    if (responseObject.Message.Equals(match))
                    {
                        Doctor = responseObject.Data;
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
                Debug.WriteLine($"Unable to get doctor profile data: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
