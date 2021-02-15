using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dto
{
    public class PersonAddEditDTO
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PersonalNumber { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required]
        public int GenderID { get; set; }
        [Required]
        public int AddressID { get; set; }
        public int AvatarID { get; set; }
        public IFormFile File { get; set; }
    }
}
