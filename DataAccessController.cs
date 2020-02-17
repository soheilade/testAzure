﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesWebApp.Models
{
    public class DataAccessController
    {
        // TODO: Add your connection string in the following statements
        private string connectionString = "Server=tcp:uniserver1364.database.windows.net,1433;Initial Catalog=unidb;Persist Security Info=False;User ID=azuresql;Password=*****;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        // Retrieve all details of courses and their modules    
        public IEnumerable<CoursesAndModules> GetAllCoursesAndModules()
        {
            List<CoursesAndModules> courseList = new List<CoursesAndModules>();

            // TODO: Connect to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // TODO: Specify the SQL query to run
                SqlCommand cmd = new SqlCommand(
                    @"SELECT c.CourseName, m.ModuleTitle, s.ModuleSequence
                    FROM dbo.Courses c JOIN dbo.StudyPlans s
                    ON c.CourseID = s.CourseID
                    JOIN dbo.Modules m
                    ON m.ModuleCode = s.ModuleCode
                    ORDER BY c.CourseName, s.ModuleSequence", con);
                    cmd.CommandType = CommandType.Text;
                // TODO: Execute the query
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                // TODO: Read the data a row at a time
                while (rdr.Read())
                {
                string courseName = rdr["CourseName"].ToString();
                string moduleTitle = rdr["ModuleTitle"].ToString();
                int moduleSequence = Convert.ToInt32(rdr["ModuleSequence"]);
                CoursesAndModules course = new CoursesAndModules(courseName, moduleTitle, moduleSequence);
                courseList.Add(course);
                }
                // TODO: Close the database connection
                con.Close();
            }
            return courseList;
        }
    }
}
