using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PersonViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderID { get; set; }
        public int AddressID { get; set; }
        public int AvatarID { get; set; }
        public IFormFile File { get; set; }
    }
}
