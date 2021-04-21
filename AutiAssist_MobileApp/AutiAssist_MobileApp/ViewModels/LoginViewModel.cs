using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using AutiAssist_MobileApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email = String.Empty;
        private string password = String.Empty;
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
                var AppShellInstance = Xamarin.Forms.Shell.Current as AppShell;

                Response response = await UserService.Login(Email, Password);

                if(response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", "Credential validation error", "OK");
                }
                else
                {
                    if (response.Message.Equals("Login Successful"))
                    {
                        if (Email.Equals("lahiru111"))
                        {
                            AppShellInstance.UpdateFlyoutMenuItems("doctor");
                        }
                        else
                        {
                            AppShellInstance.UpdateFlyoutMenuItems("patient");
                        }

                        Email = String.Empty;
                        Password = String.Empty;
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                    }
                }

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
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}
