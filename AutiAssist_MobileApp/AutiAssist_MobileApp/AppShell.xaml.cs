using AutiAssist_MobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AutiAssist_MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public void UpdateFlyoutMenuItems(string user)
        {
            if (user.ToLower().Equals("patient"))
            {
                FlyoutItem_About.FlyoutItemIsVisible = true;
                FlyoutItem_Profile.FlyoutItemIsVisible = false;
            }
            else if(user.ToLower().Equals("doctor"))
            {
                FlyoutItem_About.FlyoutItemIsVisible = false;
                FlyoutItem_Profile.FlyoutItemIsVisible = true;
            }
            else
            {
                return;
            }
            

        }
    }
}
