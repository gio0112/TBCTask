using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonType
    {
        public PersonType()
        {
            People = new HashSet<Person>();
            PersonRelations = new HashSet<PersonRelation>();
        }
        public int ID { get; set; }
        public string Value { get; set; }


        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<PersonRelation> PersonRelations { get; set; }
    }
}