using Practice_Serializer_Deserializer.Models;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    public static void Main()
    {
        Employee employee = new Employee
        {
            Gender = "female",
            Name = new Name
            {
                Title = "Miss",
                First = "Jennie",
                Last = "Nichols"
            },
            Location = new Location
            {
                Street = new Street
                {
                    Number = 8929,
                    Name = "Valwood Pkwy"
                },
                City = "Billings",
                State = "Michigan",
                Country = "United States",
                Postcode = "63104",
                Coordinates = new Coordinates
                {
                    Latitude = "-69.8246",
                    Longitude = "134.8719"
                },
                Timezone = new Timezone
                {
                    Offset = "+9:30",
                    Description = "Adelaide, Darwin"
                }
            },
            Email = "jennie.nichols@example.com",
            Login = new Login
            {
                Uuid = "7a0eed16-9430-4d68-901f-c0d4c1c3bf00",
                Username = "yellowpeacock117",
                Password = "addison",
                Salt = "sld1yGtd",
                Md5 = "ab54ac4c0be9480ae8fa5e9e2a5196a3",
                Sha1 = "edcf2ce613cbdea349133c52dc2f3b83168dc51b",
                Sha256 = "48df5229235ada28389b91e60a935e4f9b73eb4bdb855ef9258a1751f10bdc5d"
            },
            Dob = new Dob
            {
                Date = DateTime.Parse("1992-03-08T15:13:16.688Z"),
                Age = 30
            },
            Registered = new Registered
            {
                Date = DateTime.Parse("2007-07-09T05:51:59.390Z"),
                Age = 14
            },
            Phone = "(272) 790-0888",
            Cell = "(489) 330-2385",
            Id = new Id
            {
                Name = "SSN",
                Value = "405-88-3636"
            },
            Picture = new Picture
            {
                Large = "https://randomuser.me/api/portraits/men/75.jpg",
                Medium = "https://randomuser.me/api/portraits/med/men/75.jpg",
                Thumbnail = "https://randomuser.me/api/portraits/thumb/men/75.jpg"
            },
            Nat = "US"
        };


        string result = CustomSerialization(employee);
        Console.WriteLine(result);
    }
    static string CustomSerialization(object obj)
    {
        StringBuilder userJson = new();
        userJson.Append("{");

        foreach (var prop in obj.GetType().GetProperties())
        {
            userJson.Append($"\"{prop.Name.ToLower()}\": ");

            if (prop.PropertyType.IsClass && prop.PropertyType.Namespace != "System")
            {
                userJson.Append(CustomSerialization(prop.GetValue(obj)));
            }
            else
            {
                if (prop.PropertyType.IsValueType && prop.PropertyType != typeof(DateTime))
                {
                    userJson.Append($"{prop.GetValue(obj)}");
                }
                else
                {
                    userJson.Append($"\"{prop.GetValue(obj)}\"");
                }
            }
            userJson.Append(", ");
        }

        // sondaki vergul ve boslugun tenzimlenmesi
        if (userJson.Length > 1)
        {
            userJson.Length -= 2;
        }

        userJson.Append("}");

        return userJson.ToString();
    }


}

