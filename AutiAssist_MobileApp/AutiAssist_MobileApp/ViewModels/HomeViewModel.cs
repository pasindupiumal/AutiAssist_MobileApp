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
            //Response response = await UserService.GetUserByUsername("doctorone");
            //string match = "User retrieved for username - " + "doctorone";

           
            //if (response.Message.Equals(match))
            //{
                
            //    await Application.Current.MainPage.DisplayAlert("Success", response.Message, "OK");
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
            //}
        }
    }
}
