using System;
using GradSchooler.Database;
using GradSchooler.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Collections.Generic;

namespace GradSchooler.DBUtilities
{
    /// <summary>
    /// This class is used to query and update the database
    /// </summary>
    public class DBUtilities
    {

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
        public static DBUtilities Instance
        {
            get
            {
                //get lock & create instance of DBUtilies
                if (instance == null)
                {
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

            try
            {

                //sql1 = "INSERT INTO Account " +
                //"VALUES(email=@email, password_clr=@password, password=@password, firstName=@firstName, lastName=@lastName, accType=@accType, birthday=@birthday)";
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sql = "INSERT INTO Account " +
                    "VALUES ('" + acnt.email + "', '" + acnt.password + "', '" + acnt.password + "', '" + acnt.firstName + "', '" +
                      acnt.lastName + "', 'U', '" + acnt.birthday + "')";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.ExecuteNonQuery(); //to add to account

                    return true;
                }

            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }//end createAccount

        /// <summary>
        /// 
        /// </summary>
        public Boolean deleteAccount(String email)
        {
            //insert into database
            string sql = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sql = "DELETE FROM Account " +
                    "WHERE email='" + email + "'";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }//end addFavUniversity

        public Boolean addFavUniversity(String pEmail, String uni)
        {
            //insert into database
            string sqlP = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sqlP = "INSERT INTO FavUniversities " +
                    "VALUES ('" + pEmail + "', '" + uni + "')";                  

                    MySqlCommand cmd2 = new MySqlCommand(sqlP, conn);

                    cmd2.ExecuteNonQuery(); //to add the profile

                    return true;
                }

            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }//end addFavUniversity

        public Boolean updateFavUniversity(Profile profile)
        {
            //insert into database
            string sql = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sql = "UPDATE FavUniversities " +
                    "SET gradschoolname='" + profile.favUnis + "'" +
                    "WHERE accountname='" + profile.pEmail + "'";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }//end updateFavUniversity

        /// <summary>
        /// 
        /// </summary>
        public Boolean deleteFavUniversity(Profile profile)
        {
            //insert into database
            string sql = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sql = "DELETE FROM FavUniversities " +
                    "WHERE accountname='" + profile.pEmail + "' AND gradschoolname='" + profile.favUnis + "'";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }//end addFavUniversity


        /// <summary>
        /// 
        /// </summary>
        public List<University> getFavUniversities(String email)
        {
            //insert into database
            string sql = null;
            //create array for faved universities
            List<University> favUnis = new List<University>();

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    MySqlDataReader reader = null;
                    sql = "SELECT gradschoolname, fundingtype, city, state, environment " + 
                    "FROM FavUniversities, University " +
                    "WHERE accountemail='" + email + "' and gradschoolname=name";
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            University p = new University
                            {
                                name = (string)reader["gradschoolname"],
                                fundingtype = (string)reader["fundingtype"],
                                city = (string)reader["city"],
                                state = (string)reader["state"],
                                environment = (string)reader["environment"]
                            };
                            favUnis.Add(p);
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return favUnis;
            
        }//end addFavUniversity


        public Boolean UniversityPopulated()
        {
            if(getUniversities("", "").Count == 0) { return false; }
            return true;
        }

        // - getUniversityList
        public List<University> getUniversities(String keyword, String searchType)
        {
            List<University> unis = new List<University>();

            string sql = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if(conn != null)
                {
                    MySqlDataReader reader = null;
                    if (keyword == "")
                    {
                        sql = "SELECT name, fundingtype, city, state, environment, uniURL " +
                            "FROM University ";
                    }else{
                        sql = "SELECT name, fundingtype, city, state, environment, uniURL " +
                            "FROM University " +
                            "WHERE " + searchType + " LIKE '%" + keyword + "%' ";
                    }
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            University u = new University
                            {
                                name = (string)reader["name"],
                                fundingtype = (string)reader["fundingtype"],
                                city = (string)reader["city"],
                                state = (string)reader["state"],
                                environment = (string)reader["environment"],
                                url = (string)reader["uniURL"]
                            };

                            unis.Add(u);  
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.Write("This is the problem: " + e + "\n");
            }
            finally
            {
                conn.Close();
            }
            return unis;
        }

        public List<Program> getPrograms()
        {
            List<Program> pros = new List<Program>();
            string sql = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if(conn != null)
                {
                    MySqlDataReader reader = null;
                    sql = "SELECT * " +
                        "FROM Program ";
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Program p = new Program
                            {
                                programname = (string)reader["programname"],
                                degree = (string)reader["degree"],
                                acceptancerate = (float)reader["acceptancerate"],
                                instatetuition = (float)reader["instatetuition"],
                                outstatetuition = (float)reader["outstatetuition"],
                                fee = (float)reader["fee"],
                                deadline = (string)reader["deadline"],
                                schoolname = (string)reader["schoolname"]
                            };
                            pros.Add(p);
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                Console.Write("Could not display data properly" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return pros;

        }



        //method to login
        public bool loginChecker(String email, String password)
        {
            bool valid = false;

            //convert plain text to hash
            //compute hash from the bytes of text
            //get hash result after compute it
            //change it into 2 hexadecimal digits
            //for each byte
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = conn;
                    command.CommandText = "select password from Account where email=@email;";
                    command.Prepare();
                    command.Parameters.AddWithValue("@email", email);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() && reader["password"].Equals(password))
                        valid = true;
                }//if
            }
            catch (Exception e)
            {
                Debug.WriteLine("SQL EXCEPTION: " + e);
            }
            finally
            {
                conn.Close();
            }

            return valid;
        }//loginChecker

        public String getAccFirstName(String accEmail)
        {
            Debug.WriteLine("passed in accEmail: " + accEmail);
            string sql = null;
            String accFirstName = "";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sql = "SELECT firstName " +
                    "FROM Account " + 
                    "WHERE email='" + accEmail + "'";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        accFirstName = Convert.ToString(cmd.ExecuteScalar());
                        //ExecuteScalar returns the first column of the first row in the table
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.Write(e);
            }
            finally
            {
                conn.Close();
            }

            return accFirstName;
        }

        // Add a request made by the user
        public Boolean addURequest(University uR, String email, string comment){
            //insert into database
            string sqlP = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if (conn != null)
                {
                    sqlP = "INSERT INTO URequest " +
                    "VALUES ('NULL', '" + email + "', '" + comment +
                                           "', '" + uR.name + "', '" + uR.fundingtype + "', '" +
                                           uR.city + "', '" + uR.state + "', '" + uR.environment + "')";

                    MySqlCommand cmd2 = new MySqlCommand(sqlP, conn);

                    cmd2.ExecuteNonQuery(); //to add the profile

                    return true;
                }

            }
            catch (MySqlException)
            {
                Console.Write("Invalid parameters for insertion" + "\n");
            }
            finally
            {
                conn.Close();
            }
            return false;
            
        }//end addURequest

        /// <summary>
        /// close database
        /// </summary>
        public void shutDown()
        {
            conn.Close();
        }//end shutDown

    }//end DBUtilites Class
}//namspace
