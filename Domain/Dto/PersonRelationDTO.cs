﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class PersonRelationDTO
    {
        public int ID { get; set; }
        public int PersonTypeID { get; set; }
        public int PersonID { get; set; }
        public int  ContactID { get; set; }
    }
}
