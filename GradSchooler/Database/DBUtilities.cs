using System;
using GradSchooler.Database;
using MySql.Data.MySqlClient;

namespace GradSchooler.DBUtilities{
    //
    // This class is used to query and update the database
    //
    public class DBUtilities{

        private static volatile DBUtilities instance; // Singleton 
        private static object mutex = new Object(); // lock object

        private MySqlConnection conn;
        private DBConnector connector;

        // <summary>
        // constructor -- connects to & opens the database
        // </summary>
        public DBUtilities()
        {
            connector = new DBConnector();
            connector.openDB();
            conn = connector.getConn();
        }//end constructor

        // <summary>
        // check if instance of class is null, if its null,
        // create new instance.
        // returns instance of class.
        // </summary>
        public static DBUtilities Instance {
            get {
                //get lock & create instance of DBUtilies
                if(instance == null){
                    lock (mutex) instance = new DBUtilities();
                }
                return instance;
            }//end get
        }//end Instance

        // <summary>
        // close database
        // </summary>
        public void shutDown(){
            conn.Close();
        }//end shutDown

    }//end DBUtilites Class
}//namspace
