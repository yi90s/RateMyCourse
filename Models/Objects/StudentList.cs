using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class StudentList
    {
        int id { get; }
        List<Student> studentList = new List<Student>();

        public StudentList(int id)
        {
            this.id = id;
        }

        public bool AddStudentToFaculty(Student student)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var stu in studentList) if (!result)
                {
                    if (stu.id == student.id)
                    {
                        studentList.Remove(stu);
                        result = true;
                    }
                }
            studentList.Add(student);
            return result;
        }

        public void RemoveStudentFromFaculty(Student student)
        {
            studentList.Remove(student);
        }

        public List<Student> GetStudents()
        {
            return studentList;
        }
    }
}
