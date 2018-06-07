using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inc.Models
{
    public class Safety_Officer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Person Person { get; set; }
    }
}