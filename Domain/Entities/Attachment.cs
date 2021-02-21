using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Attachment
    {
        public Attachment()
        {
            People = new HashSet<Person>();
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int PersonID { get; set; }
        public ICollection<Person> People { get; set; }
    }
}