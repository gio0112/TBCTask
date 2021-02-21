using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class PersonContactDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public PersonTypeDTO PersonType { get; set; }
        public AttachmentDTO Attachment { get; set; }
    }
}
