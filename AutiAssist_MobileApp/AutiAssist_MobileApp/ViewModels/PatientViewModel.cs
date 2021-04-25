using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutiAssist_MobileApp.Views;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        private string username = String.Empty;
        private string password = String.Empty;
        private string rePassword = String.Empty;
        private string firstName = String.Empty;
        private string lastName = String.Empty;
        private string email = String.Empty;
        private string address = String.Empty;
        private string phoneNumber = String.Empty;
        private int age;
        private string gurdianName = String.Empty;
        private string assignedDoctor = String.Empty;

        public AsyncCommand BacktoLoginCommand { get; }
        public AsyncCommand PatientRegisterCommand { get; }
        public PatientViewModel()
        {
            BacktoLoginCommand = new AsyncCommand(BackToLogin);
            PatientRegisterCommand = new AsyncCommand(RegisterPatient);
        }
        private async Task BackToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async Task RegisterPatient()
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Patient registered!", "OK");
        }

        public bool PasswordMatch
        {
            get
            {
                if (Password.Equals(RePassword))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); OnPropertyChanged(nameof(PasswordMatch)); }
        }

        public string RePassword
        {
            get => rePassword;
            set { SetProperty(ref rePassword, value); OnPropertyChanged(nameof(PasswordMatch)); }
        }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        public int Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        public string GurdianName
        {
            get => gurdianName;
            set => SetProperty(ref gurdianName, value);
        }

        public string AssignedDoctor
        {
            get => assignedDoctor;
            set => SetProperty(ref assignedDoctor, value);
        }
    }
}
