using System.Linq;
using University.DAL;
using University.Infrastructure;
using University.Models;

namespace University.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext schoolContext;

        public IQueryable<Student> Students { get => schoolContext.Students; }
        public StudentRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }
        public Student GetStudent(int id)
        {
            return schoolContext.Students.Find(id);
        }

        public Student AddStudent(Student student)
        {
            var addedStudent = schoolContext.Students.Add(student);
            schoolContext.SaveChanges();

            return addedStudent;
        }

        public Student EditStudent(int studentId, Student newStudent)
        {
            var student = GetStudent(studentId);
            if (student!=null)
            {
                student.EnrollmentDate = newStudent.EnrollmentDate;
                student.LastName = newStudent.LastName;
                student.FirstMidName = newStudent.FirstMidName;
            }
            schoolContext.SaveChanges();

            return student;
        }

        public Student DeleteStudent(int id)
        {
            var student = schoolContext.Students.Find(id);
            schoolContext.Students.Remove(student);
            schoolContext.SaveChanges();
            return student;
        }

    }
}