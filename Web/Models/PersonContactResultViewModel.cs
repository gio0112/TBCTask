using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PersonContactResultViewModel
    {
        public PersonViewModel Person { get; set; }
        public ICollection<PersonContact> Contacts { get; set; }
        public ICollection<PersonType> PersonTypes { get; set; }
        public ICollection<SelectPerson> SelectPersons { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
    }
}
