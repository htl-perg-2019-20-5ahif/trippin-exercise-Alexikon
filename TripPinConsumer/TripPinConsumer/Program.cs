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
            // read users.json and make that into an object
            var jsonFileText = await File.ReadAllTextAsync("users.json");
            var users = JsonSerializer.Deserialize<List<JsonUser>>(jsonFileText);

            // iterate through users
            foreach (var user in users)
            {
                // get the user with specific username from Sercice
                var tripPinResponse = await HttpClient.GetAsync("People(\'" + user.UserName + "\')");

                // if user does not exist, create that user
                if (!tripPinResponse.IsSuccessStatusCode)
                {
                    // create ServiceUser from JsonUser
                    var serviceUser = user.CreateServiceUser();

                    // post the user to Service
                    var content = new StringContent(JsonSerializer.Serialize(serviceUser), Encoding.UTF8, "application/json");
                    var postResponse = await HttpClient.PostAsync("People", content);

                    // watch if user is successfully created
                    try
                    {
                        postResponse.EnsureSuccessStatusCode();

                        Console.WriteLine("Created Person " + user.UserName);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("Failed to create Person " + user.UserName);
                    }

                }
                // user already exists
                else
                {
                    Console.WriteLine("Person " + user.UserName + " already in TripPinRestService");
                }
            }
        }
    }
}
