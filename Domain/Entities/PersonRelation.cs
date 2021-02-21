using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonRelation
    {
        public int ID { get; set; }
        public int PersonTypeID { get; set; }
        public PersonType Type { get; set; }
        public int PersonID { get; set; }
        public Person People { get; set; }
        public int ContactID { get; set; }
        public Person Contacts { get; set; }
    }
}