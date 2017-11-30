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
        
        public Account(string emailA, string passwordA, string firstNameA,
            string lastNameA, string accTypeA, string birthdayA)
        {
            email = emailA;
            password = passwordA;
            firstName = firstNameA;
            lastName = lastNameA;
            accType = accTypeA;
            birthday = birthdayA;
        }

        public void setEmail()
        {
            
        }

       

        


    }
}