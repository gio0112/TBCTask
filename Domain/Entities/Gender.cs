using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Gender
    {
        public Gender()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}