using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TripPinConsumer
{
    public class JsonUser
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        public ServiceUser CreateServiceUser()
        {
            var city = new City();
            city.Name = this.CityName;
            city.CountryRegion = this.Country;

            var addressInfo = new AddressInfo();
            addressInfo.Address = this.Address;
            addressInfo.City = city;

            var userService = new ServiceUser();
            userService.UserName = this.UserName;
            userService.FirstName = this.FirstName;
            userService.LastName = this.LastName;
            userService.Emails = new List<string>();
            userService.Emails.Add(this.Email);
            userService.AddressInfo = new List<AddressInfo>();
            userService.AddressInfo.Add(addressInfo);

            return userService;
        }
    }
}
