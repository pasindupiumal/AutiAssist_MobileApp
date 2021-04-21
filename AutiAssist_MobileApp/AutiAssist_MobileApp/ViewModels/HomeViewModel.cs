using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutiAssist_MobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public AsyncCommand ButtonClick { get; set; }

        public HomeViewModel()
        {
            ButtonClick = new AsyncCommand(ButtonClickAction);
            
        }

        string response = "Home Page";
        public string Response
        {
            get { return response; }
            set { SetProperty(ref response, value); }
        }

        private async Task ButtonClickAction()
        {
            if(IsBusy)
            {
                return;
            }

            IsBusy = true;

            Response response = await UserService.AddNewUser();

            IsBusy = false;

            Response = response.Message;
        }
    }
}
