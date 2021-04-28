using AutiAssist_MobileApp.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using MvvmHelpers.Commands;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;
using AutiAssist_MobileApp.Views;
using AutiAssist_MobileApp.Services;

namespace AutiAssist_MobileApp.ViewModels
{
    [QueryProperty(nameof(UserObject), nameof(UserObject))]
    public class DoctorPatientReportsViewModel : BaseViewModel
    {
        private string userObject;
        private User patient;
        private string userType;
        private string username;

        public ObservableRangeCollection<Report> Reports { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand GetReportsCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Xamarin.Forms.Command ClearListCommand { get; }

        public DoctorPatientReportsViewModel()
        {
            Title = "Patient Reports";
            Reports = new ObservableRangeCollection<Report>();
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            GetReportsCommand = new AsyncCommand(GetReports);
            ClearListCommand = new Xamarin.Forms.Command(ClearList);
            userType = Preferences.Get("user_type", null);
            username = Preferences.Get("username", null);
        }

        Report selectedReport;

        bool initialLoad = false;

        public bool InitialLoad
        {
            get => initialLoad;
            set => SetProperty(ref initialLoad, value);
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

        public string Name { get; set; }

        public User Patient
        {
            get => patient;
            set => SetProperty(ref patient, value);
        }

        private void LoadPatientData(string userObject)
        {
            try
            {
                Patient = JsonConvert.DeserializeObject<User>(userObject);
                Name = Patient.PatientData.FirstName + " " + Patient.PatientData.LastName; 
            }
            catch (Exception)
            {
                Debug.WriteLine("Doctor - Patient Reports | Failed to Load Patient");
            }
        }
        public Report SelectedReport
        {
            get => selectedReport;
            set => SetProperty(ref selectedReport, value);
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
                if (userType != null)
                {
                    if (userType.Equals("Doctor"))
                    {
                        ReportListResponse response = await ReportService.GetReportsByUsername(Patient.Username);

                        string match = "All reports under " + Patient.Username + " retrieved";

                        if (response.Message.Equals(match))
                        {
                            ClearList();
                            Reports.AddRange(response.Data);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                        }
                    }
                    else
                    {
                        ReportListResponse response = await ReportService.GetReportsByUsername(username);

                        string match = "All reports under " + username + " retrieved";

                        if (response.Message.Equals(match))
                        {
                            ClearList();
                            Reports.AddRange(response.Data);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine($"Reports by username. Preferences error");
                    await Application.Current.MainPage.DisplayAlert("Error!", "Preferences error", "OK");
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get patient reports: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetReports()
        {
            if (InitialLoad)
            {
                return;
            }

            try
            {
                InitialLoad = true;
                
                if(userType != null)
                {
                    if (userType.Equals("Doctor"))
                    {
                        ReportListResponse response = await ReportService.GetReportsByUsername(Patient.Username);

                        string match = "All reports under " + Patient.Username + " retrieved";
                        
                        if (response.Message.Equals(match))
                        {
                            ClearList();
                            Reports.AddRange(response.Data);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                        }
                    }
                    else
                    {
                        ReportListResponse response = await ReportService.GetReportsByUsername(username);

                        string match = "All reports under " + username + " retrieved";

                        if (response.Message.Equals(match))
                        {
                            ClearList();
                            Reports.AddRange(response.Data);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine($"Reports by username. Preferences error");
                    await Application.Current.MainPage.DisplayAlert("Error!", "Preferences error", "OK");
                }

                InitialLoad = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get patient reports: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                InitialLoad = false;
            }
        }

        private async Task Selected(object args)
        {
            var report = args as Report;

            if (report == null)
            {
                return;
            }

            SelectedReport = null;

            report.Username = Name;
            report.PredictedAutismLevel = Patient.PatientData.Age.ToString();

            string reportObject = JsonConvert.SerializeObject(report);
            await Shell.Current.GoToAsync($"{nameof(DoctorReportDetailsPage)}?{nameof(DoctorReportDetailsViewModel.ReportObject)}={reportObject}");
        }

        private void ClearList()
        {
            Reports.Clear();
        }
    }
}
