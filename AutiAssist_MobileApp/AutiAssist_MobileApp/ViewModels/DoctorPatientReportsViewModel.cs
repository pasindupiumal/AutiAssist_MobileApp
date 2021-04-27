using AutiAssist_MobileApp.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using MvvmHelpers.Commands;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace AutiAssist_MobileApp.ViewModels
{
    [QueryProperty(nameof(UserObject), nameof(UserObject))]
    public class DoctorPatientReportsViewModel : BaseViewModel
    {
        private string userObject;
        private User patient;

        public ObservableRangeCollection<Report> Reports { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand GetReportsCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

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
        public DoctorPatientReportsViewModel()
        {
            Title = "Patient Reports";
            Reports = new ObservableRangeCollection<Report>();
            PopulateReports();
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            GetReportsCommand = new AsyncCommand(GetReports);
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
                PopulateReports();

                await Task.Delay(3000);
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
            try
            {
                InitialLoad = true;
                await Task.Delay(3000);
                PopulateReports();
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

            //string userObject = JsonConvert.SerializeObject(patient);
            await Application.Current.MainPage.DisplayAlert("Patient Selected", report.Id, "OK");
            //await Shell.Current.GoToAsync($"{nameof(DoctorPatientDetailsPage)}?{nameof(DoctorPatientDetailsViewModel.UserObject)}={userObject}");
        }

        public void ClearList()
        {
            Reports.Clear();
        }

        private void PopulateReports()
        {
            ClearList();

            var report1 = new Report
            {
                Id = "RP00001",
                UserID = "P001",
                Username = "patientone",
                SessionID = "s001",
                ActivityID = "a01",
                TimeStamp = "25 May 2021",
                ActivityResult = new ActivityResult
                {
                    ActivityID = "a01",
                    OverallScore = 90,
                    NumberOfTries = 3,
                    LevelReached = 2
                },
                FacialRecognitionResult = new FacialRecognition
                {
                    NoOfImagesProcessed = 10000,
                    OverallResult = "Happy"
                },
                vitalResult = new Vitals
                {
                    HaertBeat = "110",
                    TestField1 = "T001",
                    TestField2 = "T002"
                },
                PredictedAutismLevel = "Level 2",
                PredictedAutismScore = "0.3"
            };

            var report2 = new Report
            {
                Id = "RP00002",
                UserID = "P002",
                Username = "patienttwo",
                SessionID = "s001",
                ActivityID = "a01",
                TimeStamp = "26 May 2021",
                ActivityResult = new ActivityResult
                {
                    ActivityID = "a01",
                    OverallScore = 90,
                    NumberOfTries = 3,
                    LevelReached = 2
                },
                FacialRecognitionResult = new FacialRecognition
                {
                    NoOfImagesProcessed = 10000,
                    OverallResult = "Happy"
                },
                vitalResult = new Vitals
                {
                    HaertBeat = "110",
                    TestField1 = "T001",
                    TestField2 = "T002"
                },
                PredictedAutismLevel = "Level 2",
                PredictedAutismScore = "0.3"
            };

            var report3 = new Report
            {
                Id = "RP00003",
                UserID = "P003",
                Username = "patientthree",
                SessionID = "s001",
                ActivityID = "a01",
                TimeStamp = "27 May 2021",
                ActivityResult = new ActivityResult
                {
                    ActivityID = "a01",
                    OverallScore = 90,
                    NumberOfTries = 3,
                    LevelReached = 2
                },
                FacialRecognitionResult = new FacialRecognition
                {
                    NoOfImagesProcessed = 10000,
                    OverallResult = "Happy"
                },
                vitalResult = new Vitals
                {
                    HaertBeat = "110",
                    TestField1 = "T001",
                    TestField2 = "T002"
                },
                PredictedAutismLevel = "Level 2",
                PredictedAutismScore = "0.3"
            };

            List<Report> reports = new List<Report>();
            reports.Add(report1);
            reports.Add(report2);
            reports.Add(report3);

            Reports.AddRange(reports);
        }
    }
}
