using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Phone
    {
        public int ID { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; }
    }
}
