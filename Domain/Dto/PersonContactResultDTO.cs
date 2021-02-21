using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class PersonContactResultDTO
    {
        public PersonDTO Person { get; set; }
        public ICollection<PersonContactDTO> Contacts { get; set; }
        public ICollection<PersonTypeDTO> PersonTypes { get; set; }
        public ICollection<SelectPersonsDTO> SelectPersons { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
    }
}
