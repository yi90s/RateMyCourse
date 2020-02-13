using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cReg_WebApp.Models.Objects;

namespace cReg_WebApp.Models.SQL
{
    public static class DatabaseClient
    {
        private static readonly string connectionStatement =
            "Data Source=creg-database.coomsiajgib6.us-east-2.rds.amazonaws.com;" +
            "Initial Catalog=creg_dev;" +
            "User ID=admin;" +
            "Password=ukNyE!S^12f!ByY&";

        public static void AddStudentToSupertable(Student student)
        {
            using (var sqlConnection = new SqlConnection(connectionStatement))
            {
                sqlConnection.Open();
                var command = new SqlCommand(null, sqlConnection);

                command.CommandText =
                    "INSERT INTO Students (StudentID, Name, MajorFacultyID, MinorFacultyID) " +
                    "VALUES (@StudentID, @Name, @MajorFacultyId, @MinorFacultyId)";

                var studentIdParam = new SqlParameter("@StudentID", SqlDbType.Int, student.id);
                studentIdParam.Value = student.id;
                command.Parameters.Add(studentIdParam);

                var nameParam = new SqlParameter("@Name", SqlDbType.Text, student.name.Length);
                nameParam.Value = student.name;
                command.Parameters.Add(nameParam);

                var majorFacultyIdParam = new SqlParameter("@MajorFacultyId", SqlDbType.Text, 10);
                _ = student.major != null ? majorFacultyIdParam.Value = student.major.id.ToString() : majorFacultyIdParam.Value = DBNull.Value.ToString();
                command.Parameters.Add(majorFacultyIdParam);

                var minorFacultyIdParam = new SqlParameter("@MinorFacultyId", SqlDbType.Text, 10);
                _ = student.minor != null ? minorFacultyIdParam.Value = student.minor.id.ToString() : minorFacultyIdParam.Value = DBNull.Value.ToString();
                command.Parameters.Add(minorFacultyIdParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        public static void AddCourseToSupertable(Course course)
        {
            using (var sqlConnection = new SqlConnection(connectionStatement))
            {
                sqlConnection.Open();
                var command = new SqlCommand(null, sqlConnection);

                command.CommandText =
                    "INSERT INTO Courses (CourseID, SectionID, Name, Description) " +
                    "VALUES (@CourseID, @SectionID, @Name, @Description)";

                var courseIDParam = new SqlParameter("@CourseID", SqlDbType.Text, course.id.Length);
                courseIDParam.Value = course.id;
                command.Parameters.Add(courseIDParam);

                var sectionIDParam = new SqlParameter("@SectionID", SqlDbType.Text, course.sectionId.Length);
                sectionIDParam.Value = course.sectionId;
                command.Parameters.Add(sectionIDParam);

                var nameParam = new SqlParameter("@Name", SqlDbType.Text, course.name.Length);
                nameParam.Value = course.name;
                command.Parameters.Add(nameParam);

                var descParam = new SqlParameter("@Description", SqlDbType.Text, course.desc.Length);
                descParam.Value = course.desc;
                command.Parameters.Add(descParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        public static List<Course> GetListOfAllCourses() {
            List<Course> courseList = new List<Course>();
            using (var sqlConnection = new SqlConnection(connectionStatement))
            {
                sqlConnection.Open();
                var command = new SqlCommand(null, sqlConnection);

                command.CommandText =
                    "SELECT * FROM Courses";

                command.Prepare();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) {
                        Course course = new Course(reader.GetString(2), reader.GetString(0), reader.GetString(1), reader.GetString(3));
                        courseList.Add(course);
                    }
                }
            }

            return courseList;
        }
    }
}

/*
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
            sqlConnection = new SqlConnection(
                "Data Source=creg-database.coomsiajgib6.us-east-2.rds.amazonaws.com;" +
                "Initial Catalog=creg_dev;" +
                "User ID=admin;" +
                "Password=ukNyE!S^12f!ByY&");
        }

        public static void InsertStudentIntoTable(Student student)
        {
            sqlConnection.Open();
            var insertStudentCommand = CreateInsertStudentCommand(student);
            insertStudentCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void InsertCourseIntoTable(Course course)
        {
            sqlConnection.Open();
            var insertCourseCommand = CreateInsertCourseCommand(course);
            insertCourseCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void InsertFacultyIntoTable(Faculty faculty)
        {
            sqlConnection.Open();
            var insertCourseCommand = CreateInsertFacultyCommand(faculty);
            insertCourseCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private static SqlCommand CreateInsertStudentCommand(Student student)
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertStudent";
            command.Parameters.AddWithValue("@StudentID", student.id.ToString());
            command.Parameters.AddWithValue("@StudentName", student.name);
            _ = student.major != null ? command.Parameters.AddWithValue("@MajorFacultyID", student.major.id) : command.Parameters.AddWithValue("@MajorFacultyID", DBNull.Value);
            _ = student.minor != null ? command.Parameters.AddWithValue("@MinorFacultyID", student.minor.id) : command.Parameters.AddWithValue("@MinorFacultyID", DBNull.Value);

            return command;
        }

        private static SqlCommand CreateInsertCourseCommand(Course course)
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertCourse";
            command.Parameters.AddWithValue("@CourseID", course.id.ToString());
            command.Parameters.AddWithValue("@CourseName", course.name);
            command.Parameters.AddWithValue("@CourseSectionID", course.sectionId);
            _ = course.desc != null ? command.Parameters.AddWithValue("@CourseDesc", course.desc) : command.Parameters.AddWithValue("@CourseDesc", DBNull.Value);
            return command;
        }

        public static SqlCommand CreateInsertFacultyCommand(Faculty faculty)
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertFaculty";
            command.Parameters.AddWithValue("@FacultyID", faculty.id.ToString());
            command.Parameters.AddWithValue("@FacultyName", faculty.name);
            return command;
        }
    }
}
*/
