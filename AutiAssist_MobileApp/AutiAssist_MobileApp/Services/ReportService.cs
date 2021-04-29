using AutiAssist_MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutiAssist_MobileApp.Services
{
    class ReportService
    {
        static string BaseURL = "http://10.0.2.2:3103";
        static HttpClient client;
        static Random random;
        static ReportService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL)
            };
        }

        public static async Task<ReportListResponse> GetReportsByUsername(string username)
        {
            ReportListResponse failedResponse = null;

            try
            {
                var response = await client.GetAsync($"Reports/username/{username}");
                var getReportsResponse = await response.Content.ReadAsStringAsync();
                var defaultResponseObject = JsonConvert.DeserializeObject<ReportListResponse>(getReportsResponse);

                if (!response.IsSuccessStatusCode)
                {
                    failedResponse = new ReportListResponse();
                    failedResponse.Message = "Error connecting to server";
                    Debug.WriteLine($"Error connecting to server");
                    return failedResponse;
                }
                else
                {
                    return defaultResponseObject;
                }
            }
            catch (Exception ex)
            {
                failedResponse.Message = "Retrieving reports by username operation failed. Exception occured";
                Debug.WriteLine($"Reports retrieval error : {ex.Message}");
                return failedResponse;
            }
        }

        public static async Task<FrequentActivityResponse> GetFrequentActivities()
        {
            FrequentActivityResponse failedResponse = null;

            try
            {
                var response = await client.GetAsync($"Frequent/");
                var getFAResponse = await response.Content.ReadAsStringAsync();
                var defaultResponseObject = JsonConvert.DeserializeObject<FrequentActivityResponse>(getFAResponse);

                if (!response.IsSuccessStatusCode)
                {
                    failedResponse = new FrequentActivityResponse();
                    failedResponse.Message = "Error connecting to server";
                    Debug.WriteLine($"Error connecting to server");
                    return failedResponse;
                }
                else
                {
                    return defaultResponseObject;
                }
            }
            catch (Exception ex)
            {
                failedResponse.Message = "Retrieving frequent activities operation failed. Exception occured";
                Debug.WriteLine($"Frequent activities retrieval error : {ex.Message}");
                return failedResponse;
            }
        }
    }
}
