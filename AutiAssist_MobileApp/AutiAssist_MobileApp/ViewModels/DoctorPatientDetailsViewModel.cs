using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    [QueryProperty(nameof(UserObject), nameof(UserObject))]
    public class DoctorPatientDetailsViewModel : BaseViewModel
    {
        private string userObject;
        private User patient;

        public DoctorPatientDetailsViewModel()
        {
            //Title = "Patient Details";
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

        public User Patient
        {
            get => patient;
            set => SetProperty(ref patient, value);
        }

        public void LoadPatientData(string userObject)
        {
            try
            {
                Patient = JsonConvert.DeserializeObject<User>(userObject);
                Title = Patient.Username;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Patient");
            }
        }
    }
}
