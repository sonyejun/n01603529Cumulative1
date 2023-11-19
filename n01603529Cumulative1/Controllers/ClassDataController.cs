using MySql.Data.MySqlClient;
using n01603529Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01603529Cumulative1.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Classes table of database.
        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassData/LlistClass</example>
        /// <returns>
        /// A list of Classes (all data)
        /// </returns>

        [HttpGet]
        public IEnumerable<Class> LlistClass()
        {
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Class
            List<Class> Classes = new List<Class> { };

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                long TeacherId = (long)ResultSet["teacherid"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = ResultSet["classname"].ToString();

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;

                //Add the Class to the List
                Classes.Add(NewClass);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Classes
            return Classes;
        }
    }
}
