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

namespace AutiAssist_MobileApp.ViewModels
{
    public class DoctorPatientsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Patients { get; set; }
        public AsyncCommand RefreshCommand { get; }
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
            Task.Run(async () => await GetPatients());
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
                var patients = await UserService.GetPatientsByDoctor("doctorone");
                
                Patients.AddRange(patients.Data);

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
                Patients.Clear();
                var patients = await UserService.GetPatientsByDoctor("doctorone");

                Patients.AddRange(patients.Data);

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

            await Application.Current.MainPage.DisplayAlert("Patient Selected", patient.Username, "OK");
        }
    }
}
