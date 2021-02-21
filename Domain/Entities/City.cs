using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class City
    {
        public City()
        {
            People = new HashSet<Person>();
        }
        public int ID { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}