using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime Birthday { get; set; }

        public int Age { get; set; }
    }
}