using AutiAssist_MobileApp.Models;
using AutiAssist_MobileApp.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace AutiAssist_MobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private FrequentActivity frequentActivity;

        public FrequentActivity FrequentActivity
        {
            get => frequentActivity;
            set => SetProperty(ref frequentActivity, value);
        }
        public AsyncCommand GetActivitiesCommand { get; set; }
        public AsyncCommand TrainDataCommand { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            GetActivitiesCommand = new AsyncCommand(GetActivities);
            TrainDataCommand = new AsyncCommand(UpdateData);
        }

        private async Task GetActivities()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                //await Task.Delay(4000);

                FrequentActivityResponse responseObject = await ReportService.GetFrequentActivities();

                string match = "All Data retrieved";

                if (responseObject.Message.Equals(match))
                {
                    FrequentActivity = responseObject.Data;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", responseObject.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get frequent activities data: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task UpdateData()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                //await Task.Delay(4000);

                string responseObject = await ScoringModelService.Train();

                string match = "Data Updated Successfully";

                if (responseObject.Equals(match))
                {
                    await GetActivitiesCommand.ExecuteAsync();
                    await Application.Current.MainPage.DisplayAlert("Success", "Trained and updated successfully", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", responseObject, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to perform train data operation: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
