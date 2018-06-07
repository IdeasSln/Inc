using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inc.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Be_Id { get; set; }
        public Incident_Type Incident_Type { get; set; }
        public string Report_Date { get; set; }
        public string Incident_Occurance_Date { get; set; }
        public Location Incident_Location { get; set; }
        public Person Complainant { get; set; }
        public Disposition Disposition { get; set; }
        public Safety_Officer Report_Written_By { get; set; }
        public Safety_Officer Report_Reviewed_By { get; set; }
        public List<Person_Of_Interest> Person_Of_Interest { get; set; }
        public List<Equipment> Equipment { get; set; }
        public string Narrative { get; set; }
    }
}