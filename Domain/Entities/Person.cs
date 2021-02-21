using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Person
    {
        public Person()
        {

            PersonRelations = new HashSet<PersonRelation>();
            Phones = new HashSet<Phone>();
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderID { get; set; }
        public virtual Gender Gender { get; set; }
        public int AddressID { get; set; }
        public City Address { get; set; }
        public int? AttachmentID { get; set; }
        public Attachment? Attachment { get; set; }
        public ICollection<PersonRelation> PersonRelations { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}


