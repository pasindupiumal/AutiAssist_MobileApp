using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutiAssist_MobileApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;
        public AsyncCommand LoginCommand { get; }
        public AsyncCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new AsyncCommand(OnLoginClicked);
            RegisterCommand = new AsyncCommand(OnRegisterClicked);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private async Task OnLoginClicked()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                await Task.Delay(5000);
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Authentication error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
