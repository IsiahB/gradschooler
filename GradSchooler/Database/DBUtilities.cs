using System;
using GradSchooler.Database;
using GradSchooler.Models;
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
        /// <returns>Whether or not the account was successfully created.</returns>
        /// <param name="acnt">Account.</param>
        public Boolean createAccount(Account acnt)
        {
            //insert into database
            string sql = null;
            //string sql1 = null;

            try{
               
                //sql1 = "INSERT INTO Account " +
                    //"VALUES(email=@email, password_clr=@password, password=@password, firstName=@firstName, lastName=@lastName, accType=@accType, birthday=@birthday)";

                sql = "INSERT INTO Account " +
                    "VALUES ('" + acnt.email + "', '" + acnt.password + "', '" + acnt.password + "', '" + acnt.firstName + "', '" +
                                      acnt.lastName + "', 'U', '" + acnt.birthday + "')";
                
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                Console.Write("@email" + acnt.email + "\n");
                Console.Write("@password_clr" + acnt.password + "\n");
                Console.Write("@password" + acnt.password + "\n");
                Console.Write("@firstName" + acnt.firstName + "\n");
                Console.Write("@lastName" + acnt.lastName + "\n");
                Console.Write("@birthday" + acnt.birthday + "\n");

                //cmd.Parameters.AddWithValue("@email", acnt.email);
                //cmd.Parameters.AddWithValue("@password_clr", acnt.password);
                //cmd.Parameters.AddWithValue("@password", acnt.password);
                //cmd.Parameters.AddWithValue("@firstName", acnt.firstName);
                //cmd.Parameters.AddWithValue("@lastName", acnt.lastName);
                //cmd.Parameters.AddWithValue("@accType", "U");
                //cmd.Parameters.AddWithValue("@birthday", acnt.birthday);

                Console.Write("Here" + "\n");
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion");
            }
            return false;
        }//end createAccount

        /// <summary>
        /// close database
        /// </summary>
        public void shutDown()
        {
            conn.Close();
        }//end shutDown

    }//end DBUtilites Class
}//namspace
