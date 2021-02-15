using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderID { get; set; }
        public int AddressID { get; set; }
        public Gender Gender { get; set; }
        public City Address { get; set; }
        public Attachment Avatar { get; set; }
        public ICollection<Phone> ContactInfo { get; set; }
    }
}