using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradSchooler.App_Start {
    public class DBStartup {

        //USE THIS TO FILL THE DATABASE WITH STUFF
        //WILL ONLY BE CALLED ON STARUP
        public static void startUp() {
            DBUtilities.DBUtilities.Instance.addUniversity(); //adds universities to the database
        }//startup
    }//class
}//namespace