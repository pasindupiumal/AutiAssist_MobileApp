using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutiAssist_MobileApp.Services
{
    class ScoringModelService
    {
        static string BaseURL = "http://10.0.2.2:5001";
        static HttpClient client;
        static ScoringModelService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL)
            };
        }

        public static async Task<string> Train()
        {
            try
            {
                var response = await client.GetAsync("getValues");
                var trainResponse = await response.Content.ReadAsStringAsync();
                var AddUserStatus = JsonConvert.DeserializeObject<string>(trainResponse);

                if (!response.IsSuccessStatusCode)
                {
                    string returnValue = "Error connecting to server";
                    Debug.WriteLine($"Error connecting to server");
                    return returnValue;
                }
                else
                {
                    return AddUserStatus;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Train data operation failed : {ex.Message}");
                return "Error connecting to server";
            }
        }
    }
}
