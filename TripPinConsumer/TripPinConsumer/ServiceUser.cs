using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TripPinConsumer
{
    public class ServiceUser
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
        
        [JsonPropertyName("Emails")]
        public List<string> Emails { get; set; }
        
        [JsonPropertyName("AddressInfo")]
        public List<AddressInfo> AddressInfo { get; set; }
    }

    public class AddressInfo
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("City")]
        public City City { get; set; }
    }

    public class City
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CountryRegion")]
        public string CountryRegion { get; set; }
    }
}
