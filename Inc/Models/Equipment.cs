using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inc.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public float Value { get; set; }
        public string Occurance_Date { get; set; }
        public string Description { get; set; }
        public Equipment_Type Type { get; set;}
        public Equipment_Status Status { get; set; }

       

        public List<Equipment_Photo> Pictures { get; set; }
    }
}