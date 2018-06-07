using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inc.Models
{
    public class POI_Photo
    {
        public int Id { get; set; }
        public int POI_Id { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }
    }
}