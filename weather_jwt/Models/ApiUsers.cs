using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather_jwt.Models
{
    public class ApiUsers
    {
        public static List<ApiUser> users = new()
        {
            new ApiUser()
            {
                Id = 1,
                Name = "leyla",
                Code = "12345",
                Role = "Admin"
            },
            new ApiUser()
            {
                Id = 2,
                Name = "Test",
                Code = "13579",
                Role = "Customer"
            }
        };
    }
}
