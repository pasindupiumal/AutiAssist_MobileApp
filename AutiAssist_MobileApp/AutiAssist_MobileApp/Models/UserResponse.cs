using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutiAssist_MobileApp.Models
{
    public class UserResponse
    {
        [JsonProperty("data")]
        public IEnumerable<User> Data { get; set; }

        [JsonProperty("message")]
        public dynamic Message { get; set; }
    }


}
