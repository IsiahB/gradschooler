﻿using System;
using GradSchooler.Database;
using GradSchooler.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
            //string sql1 = null;

            try
            {

                //sql1 = "INSERT INTO Account " +
                //"VALUES(email=@email, password_clr=@password, password=@password, firstName=@firstName, lastName=@lastName, accType=@accType, birthday=@birthday)";
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if(conn != null)
                {
                    sql = "INSERT INTO Account " +
                    "VALUES ('" + acnt.email + "', '" + acnt.password + "', '" + acnt.password + "', '" + acnt.firstName + "', '" +
                      acnt.lastName + "', 'U', '" + acnt.birthday + "')";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    //cmd.Parameters.AddWithValue("@email", acnt.email);
                    //cmd.Parameters.AddWithValue("@password_clr", acnt.password);
                    //cmd.Parameters.AddWithValue("@password", acnt.password);
                    //cmd.Parameters.AddWithValue("@firstName", acnt.firstName);
                    //cmd.Parameters.AddWithValue("@lastName", acnt.lastName);
                    //cmd.Parameters.AddWithValue("@accType", "U");
                    //cmd.Parameters.AddWithValue("@birthday", acnt.birthday);

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
        }//end createAccount

        public University[] displayUniversities(University[] unis)
        {
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
                        "FROM University ";
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    using (reader = command.ExecuteReader())
                    {
                        int b = 0;
                        while (reader.Read() && b < unis.Length)
                        {
                            University u = new University
                            {
                                name = (string)reader["name"],
                                fundingtype = (string)reader["fundingtype"],
                                city = (string)reader["city"],
                                state = (string)reader["state"],
                                environment = (string)reader["environment"]
                            };

                            //Console.Write("b value:" + b + "  ");
                            unis[b] = u;
                            //Console.Write("name: " + unis[b].name + "\n");
                            b++;
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
            return unis;
        }

        public Program[] displayPrograms(Program[] pros)
        {
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
                        int i = 0;
                        while (reader.Read() && i < pros.Length)
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
                            pros[i] = p;
                            i++;
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

        public int tableSizes(string tablename)
        {
            string sql = null;
            int size = 0;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close(); //just incase it is broken
                    conn.Open(); //open the database connection
                }//if

                if(conn != null)
                {
                    sql = "SELECT COUNT(*) " +
                    "FROM " + tablename;

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        size = Convert.ToInt32(cmd.ExecuteScalar());
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

            return size;
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

        /// <summary>
        /// close database
        /// </summary>
        public void shutDown()
        {
            conn.Close();
        }//end shutDown

    }//end DBUtilites Class
}//namspace
