using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CustomeError
    {
        public Error[] Errors { get; set; }
        public string status { get; set; }
    }

    public class Error
    {
        public string Key { get; set; }
        public string[] Value { get; set; }
    }
}
