using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TripPinConsumer
{
    class Program
    {
        private static readonly string ServiceKey = "(S(qptrvmrmtuqsokow2p1ey33l))";
        private static readonly HttpClient HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://services.odata.org/TripPinRESTierService/" + ServiceKey + "/")
        };

        static async Task Main(string[] args)
        {
            // JSON Deserializer kann objekt nicht ´deserializen
            var jsonFileText = await File.ReadAllTextAsync("users.json");
            var users = JsonSerializer.Deserialize<List<JsonUser>>(jsonFileText);

            foreach (var user in users)
            {
                var tripPinResponse = await HttpClient.GetAsync("People(\'" + user.UserName + "\')");

                if(!tripPinResponse.IsSuccessStatusCode)
                {
                    var city = new City();
                    city.Name = user.CityName;
                    city.CountryRegion = user.Country;

                    var addressInfo = new AddressInfo();
                    addressInfo.Address = user.Address;
                    addressInfo.City = city;

                    var userService = new ServiceUser();
                    userService.UserName = user.UserName;
                    userService.FirstName = user.FirstName;
                    userService.LastName = user.LastName;
                    userService.Emails = new List<string>();
                    userService.Emails.Add(user.Email);
                    userService.AddressInfo = new List<AddressInfo>();
                    userService.AddressInfo.Add(addressInfo);

                    var content = new StringContent(JsonSerializer.Serialize(userService), Encoding.UTF8, "application/json");
                    var postResponse = await HttpClient.PostAsync("People", content);
                    try
                    {
                        postResponse.EnsureSuccessStatusCode();

                        Console.WriteLine("Created Person " + user.UserName);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Failed to create Person " + user.UserName);
                    }

                }
                else
                {
                    Console.WriteLine("Person " + user.UserName + " already in TripPinRestService");
                }
            }
        }
    }
}
