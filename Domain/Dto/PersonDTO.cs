﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class PersonDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderID { get; set; }
        public GenderDTO Gender { get; set; }
        public int AddressID { get; set; }
        public CityDTO Address { get; set; }
        public AttachmentDTO Attachment { get; set; }
        public ICollection<PhoneDTO> Phones { get; set; }
    }
}
