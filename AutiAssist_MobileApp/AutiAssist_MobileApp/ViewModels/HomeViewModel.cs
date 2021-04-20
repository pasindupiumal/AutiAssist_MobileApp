using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutiAssist_MobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command ButtonClick { get; set; }

        public HomeViewModel()
        {
            ButtonClick = new Command(ButtonClickAction);
            
        }

        void ButtonClickAction()
        {
            //var AppShellInstance = Xamarin.Forms.Shell.Current as AppShell;
            //AppShellInstance.UpdateFlyoutMenuItems();

        }
    }
}
