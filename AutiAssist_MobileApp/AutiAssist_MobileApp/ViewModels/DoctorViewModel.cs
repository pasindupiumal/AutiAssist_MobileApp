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
    public class DoctorViewModel : BaseViewModel
    {
        Random random = new Random();

        private string username = String.Empty;
        private string password = String.Empty;
        private string rePassword = String.Empty;
        private string firstName = String.Empty;
        private string lastName = String.Empty;
        private string email = String.Empty;
        private string specialization = String.Empty;
        private string address = String.Empty;
        private string nic = String.Empty;
        private string phoneNumber = String.Empty;
        private string slmcRegNo = String.Empty;
        private Color usernameColor = Color.Black;
        private Color passwordColor = Color.Black;
        private Color firstNameColor = Color.Black;
        private Color lastNameColor = Color.Black;
        private Color emailColor = Color.Black;
        private Color specializationColor = Color.Black;
        private Color addressColor = Color.Black;
        private Color nicColor = Color.Black;
        private Color phoneNumberColor = Color.Black;
        private Color slmcRegNoColor = Color.Black;
        public AsyncCommand BacktoLoginCommand { get; }
        public AsyncCommand DoctorRegisterCommand { get; }

        public DoctorViewModel()
        {
            BacktoLoginCommand = new AsyncCommand(BackToLogin);
            DoctorRegisterCommand = new AsyncCommand(RegisterDoctor);
        }
        private async Task BackToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async Task RegisterDoctor()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                if (Username.Length != 0 && Password.Length != 0 && FirstName.Length != 0 && LastName.Length != 0 && Email.Length != 0 && Specialization.Length != 0 &&
                Address.Length != 0 && NIC.Length != 0 && PhoneNumber.Length != 0 && SLMCRegNo.Length != 0 && RePasswordColor == Color.Black)
                {
                    int ranNum = random.Next(1000000);
                    string userID = "D" + ranNum.ToString();

                    var user = new User
                    {
                        Id = userID,
                        Username = Username,
                        Password = Password,
                        UserType = "Doctor",
                        DoctorData = new Doctor
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            Email = Email,
                            Specialization = Specialization,
                            Address = Address,
                            NIC = NIC,
                            SlmcRegNo = SLMCRegNo,
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
                    if (Specialization.Length == 0)
                    {
                        SpecializationColor = Color.Red;
                    }
                    if (Address.Length == 0)
                    {
                        AddressColor = Color.Red;
                    }
                    if (NIC.Length == 0)
                    {
                        NicColor = Color.Red;
                    }
                    if (PhoneNumber.Length == 0)
                    {
                        PhoneNumberColor = Color.Red;
                    }
                    if (SLMCRegNo.Length == 0)
                    {
                        SlmcRegNoColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Doctor Registration error: {ex.Message}");
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

        public Color SpecializationColor
        {
            get => specializationColor;
            set => SetProperty(ref specializationColor, value);
        }

        public Color AddressColor
        {
            get => addressColor;
            set => SetProperty(ref addressColor, value);
        }

        public Color NicColor
        {
            get => nicColor;
            set => SetProperty(ref nicColor, value);
        }

        public Color PhoneNumberColor
        {
            get => phoneNumberColor;
            set => SetProperty(ref phoneNumberColor, value);
        }

        public Color SlmcRegNoColor
        {
            get => slmcRegNoColor;
            set => SetProperty(ref slmcRegNoColor, value);
        }

        public string Username
        {
            get => username;
            set { SetProperty(ref username, value); }
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

        public string Specialization
        {
            get => specialization;
            set => SetProperty(ref specialization, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string NIC
        {
            get => nic;
            set => SetProperty(ref nic, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        public string SLMCRegNo
        {
            get => slmcRegNo;
            set => SetProperty(ref slmcRegNo, value);
        }
    }
}
