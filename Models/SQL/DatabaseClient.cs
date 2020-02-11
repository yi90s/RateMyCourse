using cReg_WebApp.Controllers;
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
                "Data Source=creg-database.coomsiajgib6.us-east-2.rds.amazonaws.com;Initial Catalog=creg_dev;User ID=admin;Password=ukNyE!S^12f!ByY&");
        }

        public static void InsertStudentIntoTable(Student student)
        {
            sqlConnection.Open();
            var insertStudentCommand = CreateInsertStudentCommand(student);
            insertStudentCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static SqlCommand CreateInsertStudentCommand(Student student)
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertStudent";
            command.Parameters.AddWithValue("@StudentID", student.Id.ToString());
            command.Parameters.AddWithValue("@StudentName", student.Name);
            _ = student.Major != null ? command.Parameters.AddWithValue("@MajorFacultyName", student.Major.Name) : command.Parameters.AddWithValue("@MajorFacultyName", DBNull.Value);
            _ = student.Major != null ? command.Parameters.AddWithValue("@MajorFacultyID", student.Major.Id) : command.Parameters.AddWithValue("@MajorFacultyID", DBNull.Value);
            _ = student.Minor != null ? command.Parameters.AddWithValue("@MinorFacultyName", student.Minor.Name) : command.Parameters.AddWithValue("@MinorFacultyName", DBNull.Value);
            _ = student.Minor != null ? command.Parameters.AddWithValue("@MinorFacultyID", student.Minor.Id) : command.Parameters.AddWithValue("@MinorFacultyID", DBNull.Value);
            command.Parameters.AddWithValue("@ShortlistID", DBNull.Value); // TODO : Decide how to implement Shortlist (if it needs unique ID etc.) - for now this is null
            return command;
        }
    }
}
