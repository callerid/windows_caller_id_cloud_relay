using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace CallerID_Cloud_Relay.Classes
{
    class CID_Database
    {
        private static SQLiteConnection databaseConnection = null;
        private static string databaseDir = Application.StartupPath + "\\cid_cloud_relay_call_log.db3";

        // Static constant queries
        private static string creationString = "CREATE TABLE callLog (ID INTEGER PRIMARY KEY AUTOINCREMENT, line varchar(2), io varchar(1), se varchar(1), duration varchar(4), ring varchar(2), theDateTime varchar(20), number varchar(20), name varchar(20));";

        public CID_Database()
        {
            CreateDatabase();
            CreateLog();
        }

        public bool CreateDatabase()
        {
            if (File.Exists(databaseDir))
            {
                Console.WriteLine("Database not created because it already exists.");
                return true;
            }
            else
            {
                // Creates Database db3 File
                try
                {
                    SQLiteConnection.CreateFile(databaseDir);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to create database. Ex: " + ex.ToString());
                    return false;
                }
            }
        }

        public bool OpenDatabase()
        {
            // Connect to database
            databaseConnection = new SQLiteConnection
            {
                ConnectionString = @"Data Source=" + databaseDir
            };

            // Log into log database
            try
            {
                databaseConnection.Open();
                return true;

            }
            catch (Exception)
            {
                Console.WriteLine("Database failed to open.");
                return false;
            }
        }

        public void CloseDatabase()
        {
            databaseConnection.Close();
            Console.WriteLine("Database closed.");
        }

        public bool CreateLog()
        {
            if (OpenDatabase())
            {
                return ExecuteQuery(creationString);
            }
            else
            {
                return false;
            }            
        }

        public bool InsertIntoLog(string line, string dateTime, string number, string name, string io,
            string se, string status, string duration, string ring)
        {
            try
            {

                string insertQuery = "INSERT INTO callLog (" +

                    "line," +
                    "io," +
                    "se," +
                    "duration," +
                    "ring," +
                    "theDateTime," +
                    "number," +
                    "name " +

                    ") VALUES (" +

                    "'" + line + "'," +
                    "'" + io + "'," +
                    "'" + se + "'," +
                    "'" + duration + "'," +
                    "'" + ring + "'," +
                    "'" + dateTime + "'," +
                    "'" + number + "'," +
                    "'" + name + "'" +

                ");";

                ExecuteQuery(insertQuery);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add to log. Ex: " + ex.ToString());
                return false;
            }
        }

        public bool ClearLog()
        {
            try
            {
                ExecuteQuery("DELETE * FROM callLog;");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not clear: " + ex.ToString());
                return false;
            }
        }

        public bool ExecuteQuery(string query)
        {

            if (!OpenDatabase())
            {
                Console.WriteLine("Database couldn't be opened.");
                return false;
            }

            var myCommand = new SQLiteCommand(query, databaseConnection);
            if (databaseConnection.State == ConnectionState.Open)
            {
                try
                {
                    myCommand.ExecuteNonQuery();
                    databaseConnection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                    return false;
                }

            }
            else
            {
                Console.WriteLine("Database not opened for query");
                return false;
            }
        }

        public DataTable LoadLog()
        {

            if (!OpenDatabase()) return null;

            string query = "SELECT * FROM callLog LIMIT 100;";

            DataTable rtnData = new DataTable();
            rtnData.Columns.Add("line");
            rtnData.Columns.Add("io");
            rtnData.Columns.Add("se");
            rtnData.Columns.Add("duration");
            rtnData.Columns.Add("ring");
            rtnData.Columns.Add("theDateTime");
            rtnData.Columns.Add("number");
            rtnData.Columns.Add("name");

            var myCommand = new SQLiteCommand(query, databaseConnection);
            try
            {
                using (var reader = myCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get data from reader
                        string line = reader["line"].ToString();
                        string io = reader["io"].ToString();
                        string se = reader["se"].ToString();
                        string duration = reader["duration"].ToString();
                        string ring = reader["ring"].ToString();
                        string theDateTime = reader["theDateTime"].ToString();
                        string number = reader["number"].ToString();
                        string name = reader["name"].ToString();

                        rtnData.Rows.Add(line, io, se, duration, ring, theDateTime, number, name);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("->" + query + "<-- failed. Ex: " + ex.ToString());
                return null;
            }

            return rtnData;

        }

    }
}
