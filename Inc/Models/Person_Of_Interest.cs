using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inc.Models
{
    public class Person_Of_Interest
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public POI_Type Type { get; set; }
        public string Comments { get; set; }
        public string Photo { get; set; }

        public List<POI_Photo> Pictures { get; set; }
    }
}