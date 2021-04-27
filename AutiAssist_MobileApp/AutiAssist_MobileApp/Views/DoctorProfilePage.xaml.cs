using AutiAssist_MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutiAssist_MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorProfilePage : ContentPage
    {
        private DoctorProfileViewModel doctorProfileViewModel;
        public DoctorProfilePage()
        {
            InitializeComponent();

            //BindingContext = doctorProfileViewModel = new DoctorProfileViewModel();
        }

        //protected async override void OnAppearing()
        //{
        //    await doctorProfileViewModel.GetProfileDataCommand.ExecuteAsync();
        //}
    }
}