using cReg_WebApp.Controllers;
using cReg_WebApp.Models.Objects;
using System;
using System.Data.SqlClient;

namespace cReg_WebApp.Models.SQL
{
    public static class DatabaseClient
    {
        // TODO: Figure out how to not hard code the connection string, etc. into here
        public static SqlConnection sqlConnection;

        public static void Initialize()
        {
            sqlConnection = new SqlConnection("Data Source=creg.clcmfom6h0l9.us-east-2.rds.amazonaws.com,1433;Initial Catalog=Creg;User ID=admin;Password=bpYL3o34rc3P0Wt7dm1a;");
        }

        //public static void InsertStudentIntoTable(Student student)
        //{
        //    sqlConnection.Open();
        //    var insertStudentCommand = CreateInsertStudentCommand(student);
        //    insertStudentCommand.ExecuteNonQuery();
        //    sqlConnection.Close();
        //}

        //public static SqlCommand CreateInsertStudentCommand(Student student)
        //{
        //    var command = sqlConnection.CreateCommand();
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    command.CommandText = "dbo.InsertStudent";
        //    command.Parameters.AddWithValue("@StudentID", student.id.ToString());
        //    command.Parameters.AddWithValue("@StudentName", student.name);
        //    _ = student.major != null ? command.Parameters.AddWithValue("@MajorFacultyID", student.major.id) : command.Parameters.AddWithValue("@MajorFacultyID", DBNull.Value);
        //    _ = student.minor != null ? command.Parameters.AddWithValue("@MinorFacultyID", student.minor.id) : command.Parameters.AddWithValue("@MinorFacultyID", DBNull.Value);
        //    command.Parameters.AddWithValue("@ShortlistID", DBNull.Value); // TODO : Decide how to implement Shortlist (if it needs unique ID etc.) - for now this is null
        //    return command;
        //}

        //public static SqlCommand CreateInsertCourseCommand(Course course)
        //{
        //    var command = sqlConnection.CreateCommand();
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    command.CommandText = "dbo.InsertCourse";
        //    command.Parameters.AddWithValue("@CourseID", course.id.ToString());
        //    command.Parameters.AddWithValue("@CourseName", course.name);
        //    command.Parameters.AddWithValue("@CourseSectionID", course.sectionId);
        //    _ = course.desc != null ? command.Parameters.AddWithValue("@CourseDesc", course.desc) : command.Parameters.AddWithValue("@CourseDesc", DBNull.Value);
        //    _ = course.preReqs != null ? command.Parameters.AddWithValue("@CoursePreReqs", course.preReqs) : command.Parameters.AddWithValue("@CourseDesc", DBNull.Value);
        //    return command;
        //}

        //public static SqlCommand CreateInsertFacultyCommand(Course course)
        //{
        //    var command = sqlConnection.CreateCommand();
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    command.CommandText = "dbo.InsertCourse";
        //    command.Parameters.AddWithValue("@CourseID", course.id.ToString());
        //    command.Parameters.AddWithValue("@CourseName", course.name);
        //    command.Parameters.AddWithValue("@CourseSectionID", course.sectionId);
        //    _ = course.desc != null ? command.Parameters.AddWithValue("@CourseDesc", course.desc) : command.Parameters.AddWithValue("@CourseDesc", DBNull.Value);
        //    _ = course.preReqs != null ? command.Parameters.AddWithValue("@CoursePreReqs", course.preReqs) : command.Parameters.AddWithValue("@CourseDesc", DBNull.Value);
        //    return command;
        //}

        public static Student findStudent(int accountNumber)
        {
            try
            {
                sqlConnection.Open();
                var command = sqlConnection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from [Student] where studentId=@sId";
                command.Parameters.AddWithValue("@sId", accountNumber);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string password = reader.GetString(1).Replace(" ", "");
                    Student newStu = new Student(reader.GetString(3), reader.GetInt32(0), 0,password);
                    sqlConnection.Close();
                    return newStu;
                }
                else
                {
                    sqlConnection.Close();
                    return null;
                }
            }catch (Exception ex)
            {
                return null;
            }
        }

    }
}
