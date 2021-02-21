using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PhoneType
    {
        public PhoneType()
        {
            Phones = new HashSet<Phone>();
        }
        public int ID { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}