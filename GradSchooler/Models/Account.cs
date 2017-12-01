using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradSchooler.Models
{
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