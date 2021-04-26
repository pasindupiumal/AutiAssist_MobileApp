using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Xamarin.Essentials;
using AutiAssist_MobileApp.Views;
using Newtonsoft.Json;

namespace AutiAssist_MobileApp.ViewModels
{
    public class DoctorPatientsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Patients { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand GetPatientsCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        User selectedPatient;

        bool initialLoad = false;

        public bool InitialLoad
        {
            get => initialLoad;
            set => SetProperty(ref initialLoad, value);
        }

        public User SelectedPatient
        {
            get => selectedPatient;
            set => SetProperty(ref selectedPatient, value);
        }
        public DoctorPatientsViewModel()
        {
            Title = "Patients";
            Patients = new ObservableRangeCollection<User>();
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            GetPatientsCommand = new AsyncCommand(GetPatients);
            //Task.Run(async () => await GetPatients());
        }

        private async Task Refresh()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                Patients.Clear();
                var doctorUsername = Preferences.Get("username", null);

                if(doctorUsername != null)
                {
                    var patients = await UserService.GetPatientsByDoctor(doctorUsername);

                    Patients.AddRange(patients.Data);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", "Error obtaining patient data", "OK");
                }
                

                IsBusy = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get patients: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetPatients()
        {
            try
            {
                InitialLoad = true;
                //await Task.Delay(4000);
                Patients.Clear();
                var doctorUsername = Preferences.Get("username", null);

                if(doctorUsername != null)
                {
                    var patients = await UserService.GetPatientsByDoctor(doctorUsername);

                    Patients.AddRange(patients.Data);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", "Error obtaining patient data", "OK");
                }
                

                InitialLoad = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get patients: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                InitialLoad = false;
            }
        }

        private async Task Selected(object args)
        {
            var patient = args as User;

            if (patient == null)
            {
                return;
            }

            SelectedPatient = null;

            string userObject = JsonConvert.SerializeObject(patient);
            //await Application.Current.MainPage.DisplayAlert("Patient Selected", patient.Username, "OK");
            await Shell.Current.GoToAsync($"{nameof(DoctorPatientDetailsPage)}?{nameof(DoctorPatientDetailsViewModel.UserObject)}={userObject}");
        }

        public void ClearList()
        {
            Patients.Clear();
        }
    }
}
