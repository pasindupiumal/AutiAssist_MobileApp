using AutiAssist_MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    [QueryProperty(nameof(ReportObject), nameof(ReportObject))]
    public class DoctorReportDetailsViewModel : BaseViewModel
    {
        private string reportObject;
        private Report report;
        public DoctorReportDetailsViewModel()
        {
            Title = "Report Details";
        }

        public string ReportObject
        {
            get => reportObject;
            set
            {
                reportObject = value;
                LoadReportData(value);
            }
        }

        public Report Report
        {
            get => report;
            set => SetProperty(ref report, value);
        }

        public void LoadReportData(string reportObject)
        {
            try
            {
                Report = JsonConvert.DeserializeObject<Report>(reportObject);
            }
            catch (Exception)
            {
                Debug.WriteLine("Doctor - Patient Report Details Page | Failed to Load Report Data");
            }
        }
    }
}
