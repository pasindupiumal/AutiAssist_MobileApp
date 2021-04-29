using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutiAssist_MobileApp.Models
{
    public class FrequentActivity
    {
        [JsonProperty("autismAssistSEQ")]
        public string AutismAssistSEQ { get; set; }

        [JsonProperty("ageCategory1")]
        public string AgeCategory1 { get; set; }

        [JsonProperty("ageActivity1")]
        public string AgeActivity1 { get; set; }

        [JsonProperty("ageCategory2")]
        public string AgeCategory2 { get; set; }

        [JsonProperty("ageActivity2")]
        public string AgeActivity2 { get; set; }

        [JsonProperty("ageCategory3")]
        public string AgeCategory3 { get; set; }

        [JsonProperty("ageActivity3")]
        public string AgeActivity3 { get; set; }
    }
}
