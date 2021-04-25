using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using AutiAssist_MobileApp.Views;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        Random random = new Random();
        private string username = String.Empty;
        private string password = String.Empty;
        private string rePassword = String.Empty;
        private string firstName = String.Empty;
        private string lastName = String.Empty;
        private string email = String.Empty;
        private string address = String.Empty;
        private string phoneNumber = String.Empty;
        private int age = 1;
        private string gurdianName = String.Empty;
        private string assignedDoctor = String.Empty;
        private Color usernameColor = Color.Black;
        private Color passwordColor = Color.Black;
        private Color rePasswordColor = Color.Black;
        private Color firstNameColor = Color.Black;
        private Color lastNameColor = Color.Black;
        private Color emailColor = Color.Black;
        private Color addressColor = Color.Black;
        private Color phoneNumberColor = Color.Black;
        private Color gurdianNameColor = Color.Black;
        private Color assignedDoctorColor = Color.Black;

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
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                if (Username.Length != 0 && Password.Length != 0 && FirstName.Length != 0 && LastName.Length != 0 && Email.Length != 0 && GurdianName.Length != 0 &&
                Address.Length != 0 && AgeColor == Color.Black && PhoneNumber.Length != 0 && AssignedDoctor.Length != 0 && RePasswordColor == Color.Black)
                {
                    int ranNum = random.Next(1000000);
                    string userID = "P" + ranNum.ToString();

                    var user = new User
                    {
                        Id = userID,
                        Username = Username,
                        Password = Password,
                        UserType = "Patient",
                        PatientData = new Patient
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            Email = Email,
                            Age = Age,
                            Address = Address,
                            GuardianName = GurdianName,
                            AssignedDoctor = AssignedDoctor,
                            PhoneNumber = PhoneNumber
                        }
                    };

                    Response response = await UserService.AddNewUser(user);

                    if (response.Message.Equals("New user added successfully"))
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Registration Successful", "OK");
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                    }
                }
                else
                {
                    if (Username.Length == 0)
                    {
                        UsernameColor = Color.Red;
                    }
                    if (Password.Length == 0)
                    {
                        PasswordColor = Color.Red;
                    }
                    if (FirstName.Length == 0)
                    {
                        FirstNameColor = Color.Red;
                    }
                    if (LastName.Length == 0)
                    {
                        LastNameColor = Color.Red;
                    }
                    if (Email.Length == 0)
                    {
                        EmailColor = Color.Red;
                    }
                    if (AssignedDoctor.Length == 0)
                    {
                        AssignedDoctorColor = Color.Red;
                    }
                    if (Address.Length == 0)
                    {
                        AddressColor = Color.Red;
                    }
                    if (GurdianName.Length == 0)
                    {
                        GurdianNameColor = Color.Red;
                    }
                    if (PhoneNumber.Length == 0)
                    {
                        PhoneNumberColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Patient Registration error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;

            }
        }

        public Color RePasswordColor
        {
            get
            {
                if (Password.Equals(RePassword))
                {
                    return Color.Black;
                }
                else
                {
                    return Color.Red;
                }
            }
        }

        public Color UsernameColor
        {
            get => usernameColor;
            set => SetProperty(ref usernameColor, value);
        }

        public Color PasswordColor
        {
            get => passwordColor;
            set => SetProperty(ref passwordColor, value);
        }

        public Color FirstNameColor
        {
            get => firstNameColor;
            set => SetProperty(ref firstNameColor, value);
        }

        public Color LastNameColor
        {
            get => lastNameColor;
            set => SetProperty(ref lastNameColor, value);
        }

        public Color EmailColor
        {
            get => emailColor;
            set => SetProperty(ref emailColor, value);
        }

        public Color AgeColor
        {
            get
            {
                if(Age <=0 || Age > 120)
                {
                    return Color.Red;
                }
                else
                {
                    return Color.Black; 
                }
            }
        }

        public Color AddressColor
        {
            get => addressColor;
            set => SetProperty(ref addressColor, value);
        }

        public Color GurdianNameColor
        {
            get => gurdianNameColor;
            set => SetProperty(ref gurdianNameColor, value);
        }

        public Color PhoneNumberColor
        {
            get => phoneNumberColor;
            set => SetProperty(ref phoneNumberColor, value);
        }

        public Color AssignedDoctorColor
        {
            get => assignedDoctorColor;
            set => SetProperty(ref assignedDoctorColor, value);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); OnPropertyChanged(nameof(RePasswordColor)); }
        }

        public string RePassword
        {
            get => rePassword;
            set { SetProperty(ref rePassword, value); OnPropertyChanged(nameof(RePasswordColor)); }
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
            set { SetProperty(ref age, value); OnPropertyChanged(nameof(AgeColor)); }
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
