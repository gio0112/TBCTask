using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AddEditPersonViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderID { get; set; }
        public IEnumerable<Gender> Gender { get; set; }
        public int AddressID { get; set; }
        public IEnumerable<City> Address { get; set; }
        public int PhoneTypeID { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<PhoneType> PhoneType { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public IFormFile File { get; set; }
        public int AttachmentID { get; set; }
    }
}
