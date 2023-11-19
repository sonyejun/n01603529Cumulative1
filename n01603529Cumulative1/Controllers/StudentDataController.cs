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
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Students table of database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudent</example>
        /// <returns>
        /// A list of Students (all data)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> LlistStudent()
        {
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students";
            
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student
            List<Student> Students = new List<Student> { };

            while (ResultSet.Read())
            {
                uint StudentId = (uint)ResultSet["studentid"];
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime Enroldate = (DateTime)ResultSet["enroldate"];

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.Enroldate = Enroldate;

                //Add the Student to the List
                Students.Add(NewStudent);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Students
            return Students;
        }
    }
}
