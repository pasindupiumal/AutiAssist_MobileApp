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

        public DoctorPatientDetailsViewModel()
        {
            
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

        public void LoadPatientData(string userObject)
        {
            try
            {
                User patient = JsonConvert.DeserializeObject<User>(userObject);
                Title = patient.Username;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Patient");
            }
        }
    }
}
