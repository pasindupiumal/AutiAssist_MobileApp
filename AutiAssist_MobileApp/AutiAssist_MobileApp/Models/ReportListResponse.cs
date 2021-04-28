using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutiAssist_MobileApp.Models
{
    public class ReportListResponse
    {
        [JsonProperty("data")]
        public IEnumerable<Report> Data { get; set; }

        [JsonProperty("message")]
        public dynamic Message { get; set; }
    }
}
