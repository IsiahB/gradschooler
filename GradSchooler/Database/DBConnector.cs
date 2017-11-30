using System;
using MySql.Data.MySqlClient;

namespace GradSchooler.Database{
    // <summary>
    // Connects to database
    // </summary>
    public class DBConnector
    {
        // log messages
        private const string SOURCE = "DBConnector";
        private const string SUCCESS = "Successfully Connected to the Database";
        private const string BAD_LOGIN = "Could not login to the Database";
        private const string BAD_CONNECTION = "Error Connecting to database. Database could not be reached.";

        // members
        private MySqlConnection conn;
        private const string server = "mal.cs.plu.edu";
        private const string userID = "grads";
        private const string password = "grads";
        private const string database = "gradschooler";

        /// <summary>
        /// default constructor
        /// </summary>
        public DBConnector(){}

        /// <summary>
        /// opens the database
        /// </summary>
        public Boolean openDB()
        {
            try
            {
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder
                {
                    Server = server,
                    UserID = userID,
                    Password = password,
                    Database = database
                };

                //create connection to database
                conn = new MySqlConnection(conn_string.ToString());
                conn.Open();
                //if (conn != null) Logger.Instance.log(SOURCE, SUCCESS);
                //else Logger.Instance.log(SOURCE, BAD_LOGIN);
                return true;
            }
            catch(MySqlException)
            {
                //Logger.Instance.error(SOURCE, BAD_CONNECTION, false);
                return false;
            }//catch

        }//end openDB

        /// <summary>
        /// Closes the instance of the database connection
        /// </summary>
        public void closeDB()
        {
            if (conn != null) conn.Close();
            conn = null;
        }//end closeDB

        /// <summary>
        /// Returns the connection object to the database
        /// </summary>
        /// <returns>The MySQL connection</returns>
        public MySqlConnection getConn()
        {
            return conn;
        }//end getConn

    }//end class DBConnector
}//namespace
