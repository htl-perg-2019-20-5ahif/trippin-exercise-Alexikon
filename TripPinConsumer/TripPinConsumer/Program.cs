using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TripPinConsumer
{
    class Program
    {
        private static HttpClient HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://services.odata.org/TripPinRESTierService/(S(3mslpb2bc0k5ufk24olpghzx))/")
        };

        static async Task Main(string[] args)
        {
            // JSON Deserializer kann objekt nicht ´deserializen
            var jsonFileText = await File.ReadAllTextAsync("users.json");
            var users = JsonSerializer.Deserialize<Users>(jsonFileText);





            //foreach (var user in users.Results)
            //{

            //}



            // Test, ob HttpClient Funktioniert
            var tripPinResponse = await HttpClient.GetAsync("People(\'russellwhyte\')");
            var dfjh = tripPinResponse.StatusCode;
            var responseBody = await tripPinResponse.Content.ReadAsStringAsync();
            Console.WriteLine(dfjh + "\n\n" + responseBody);




        }
    }
}
