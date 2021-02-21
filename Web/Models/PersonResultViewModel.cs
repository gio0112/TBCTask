using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PersonResultViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
    }
}
