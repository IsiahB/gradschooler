using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradSchooler.Models
{
    /// <summary>
    /// Account Model to represent the data of a user's Account
    /// </summary>
    public class Account
    {
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string accType { get; set; }
        public string birthday { get; set; }   
    }
}