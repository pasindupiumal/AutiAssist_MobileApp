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

        private async Task ButtonClickAction()
        {
            await UserService.Login("J", "J");

        }
    }
}
