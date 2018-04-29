using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradSchooler.Models
{
    public class Profile
    {
        public string pEmail { get; set; }
        public string[] deadlines { get; set; } //to store important dates
        public string[] favUnis { get; set; }
        public string bio { get; set; }
    }
}