using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AutiAssist_MobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public AsyncCommand ButtonClick { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            ButtonClick = new AsyncCommand(ClickHere);
        }

        async Task ClickHere()
        {
            UserResponse response = await UserService.GetPatientsByDoctor("doctorone");

            if(response.Message.Equals("Selected patients retrieved"))
            {
                //await Application.Current.MainPage.DisplayAlert("Success", "In Here, "OK");

                string name = "";

                foreach(User user in response.Data)
                {
                    name += " " + user.Username;
                }

                await Application.Current.MainPage.DisplayAlert("Success", name, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Success", response.Message, "OK");
            }
            

            
        }
    }
}
