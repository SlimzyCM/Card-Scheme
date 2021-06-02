using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CardScheme.Domain.Entities
{
    /// <summary>
    /// Class for the Json Serialization
    /// </summary>
    public class CardDetails
    {
        [JsonProperty("result")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("bin")]
        public string Bin { get; set; }

        [JsonProperty("vendor")]
        public string Scheme { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }
    }


}
