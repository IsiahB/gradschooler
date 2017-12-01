using System;
using GradSchooler.Database;
using MySql.Data.MySqlClient;

namespace GradSchooler.DBUtilities{
    /// <summary>
    /// This class is used to query and update the database
    /// </summary>
    public class DBUtilities{

        private static volatile DBUtilities instance; // Singleton 
        private static object mutex = new Object(); // lock object

        private MySqlConnection conn;
        private DBConnector connector;

        /// <summary>
        /// constructor -- connects to & opens the database
        /// </summary>
        private DBUtilities()
        {
            connector = new DBConnector();
            connector.openDB();
            conn = connector.getConn();
        }//end constructor

        /// <summary>
        /// check if instance of class is null, if its null,
        /// create new instance.
        /// returns instance of class.
        /// </summary>
        public static DBUtilities Instance {
            get {
                //get lock & create instance of DBUtilies
                if(instance == null){
                    lock (mutex) instance = new DBUtilities();
                }
                return instance;
            }//end get
        }//end Instance

        /// <summary>
        /// Execute the specified insertCommand, email, password1, password2, firstName, lastName and birthday.
        /// </summary>
        /// <returns>The execute.</returns>
        /// <param name="insertCommand">Insert command.</param>
        /// <param name="email">Email.</param>
        /// <param name="password1">Password1.</param>
        /// <param name="password2">Password2.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="birthday">Birthday.</param>
        internal void Execute(string insertCommand, string email, string password1, string password2, string firstName, string lastName, string birthday)
        {
            throw new NotImplementedException();
        }

        // <summary>
        // close database
        // </summary>
        public void shutDown(){
            conn.Close();
        }//end shutDown

    }//end DBUtilites Class
}//namspace
