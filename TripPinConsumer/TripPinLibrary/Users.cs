using System;
using System.Collections.Generic;

namespace TripPinLibrary
{
    public class Users
    {
        public List<User> Results { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}
