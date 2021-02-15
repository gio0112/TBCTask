using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Dto
{
    public class PersonResult
    {
        public List<PersonDTO> Persons { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
    }
}
