﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PersonViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public City Address { get; set; }
        public Attachment Attachment { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}
