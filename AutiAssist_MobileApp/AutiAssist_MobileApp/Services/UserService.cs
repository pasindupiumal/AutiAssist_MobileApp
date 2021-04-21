using AutiAssist_MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutiAssist_MobileApp.Services
{
    class UserService
    {
        static string BaseURL = "http://10.0.2.2:3103";
        static HttpClient client;
        static Random random;
        static UserService()
        { 
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL)
            };
        }

        public static async Task Login(string email, string password)
        {
            string eemail = "lahiru111";
            string ppassword = "password1";

            var credentials = new Credential
            {
                Username = eemail,
                Password = ppassword
            };

            var json = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users/login", content);
            var loginResonse = await response.Content.ReadAsStringAsync();
            var loginStatus = JsonConvert.DeserializeObject<Response>(loginResonse);

            if (!response.IsSuccessStatusCode)
            {
                await AppShell.Current.DisplayAlert("Error", "Error obtaining user data", "OK");
            }
            else
            {
                await AppShell.Current.DisplayAlert("Success", loginStatus.Data.ToString(), "OK");
            }

            
        }
        //public static async Task RemoveCoffee(int id)
        //{
        //    var response = await client.DeleteAsync($"api/Coffee/{id}");

        //    if (!response.IsSuccessStatusCode)
        //    {

        //    }
        //}
        //public static async Task<IEnumerable<Coffee>> GetCoffee()
        //{
        //    var json = await client.GetStringAsync("api/Coffee");
        //    var coffees = JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json);
        //    return coffees;
        //}
    }
}
