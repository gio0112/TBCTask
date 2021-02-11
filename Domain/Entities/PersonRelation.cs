using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonRelation
    {
        public int ID { get; set; }
        public PersonType Type { get; set; }
        public Person Person { get; set; }
        public Person RelationPerson { get; set; }
    }
}