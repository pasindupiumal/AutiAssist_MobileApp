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

        public static async Task<Response> Login(string username, string password)
        {
            var credentials = new Credential
            {
                Username = username,
                Password = password
            };

            Response failedResponse = null;

            try
            {
                var json = JsonConvert.SerializeObject(credentials);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Users/login", content);
                var loginResponse = await response.Content.ReadAsStringAsync();
                var loginStatus = JsonConvert.DeserializeObject<Response>(loginResponse);

                if (!response.IsSuccessStatusCode)
                {
                    failedResponse = new Response();
                    failedResponse.Data = false;
                    failedResponse.Message = "Error connecting to server";
                    Debug.WriteLine($"Error connecting to server");
                    return failedResponse;
                }
                else
                {
                    return loginStatus;
                }
            }
            catch (Exception ex)
            {
                failedResponse.Data = false;
                failedResponse.Message = "Validation failed. Exception occured";
                Debug.WriteLine($"Logging in error : {ex.Message}");
                return failedResponse;
            }
        }

        public static async Task<Response> AddNewUser(User user)
        {
            Response failedResponse = null;

            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Users/", content);
                var addNewUserResponse = await response.Content.ReadAsStringAsync();
                var AddUserStatus = JsonConvert.DeserializeObject<Response>(addNewUserResponse);

                if (!response.IsSuccessStatusCode)
                {
                    failedResponse = new Response();
                    failedResponse.Message = "Error connecting to server";
                    Debug.WriteLine($"Error connecting to server");
                    return failedResponse;      
                }
                else
                {
                    return AddUserStatus;
                }
            }
            catch (Exception ex)
            {
                failedResponse.Message = "User insertion failed. Exception occured";
                Debug.WriteLine($"New user insertion error : {ex.Message}");
                return failedResponse;
            }
        }

        public static async Task<UserResponse> GetPatientsByDoctor(string assignedDoctor)
        {
            UserResponse failedResponse = null;

            try
            {
                //var json = JsonConvert.SerializeObject(user);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"Users/patients/{assignedDoctor}", null);
                var getPatientsResponse = await response.Content.ReadAsStringAsync();
                var defaultResponseObject = JsonConvert.DeserializeObject<UserResponse>(getPatientsResponse);
                //var patients = JsonConvert.DeserializeObject<IEnumerable<User>>(defaultResponseObject.Data);

                if (!response.IsSuccessStatusCode)
                {
                    failedResponse = new UserResponse();
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
                failedResponse.Message = "Retrieving patients by doctor operation failed. Exception occured";
                Debug.WriteLine($"Patient retrieval error : {ex.Message}");
                return failedResponse;
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
