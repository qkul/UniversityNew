using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Infrastructure
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        Student GetStudent(int id);
        Student AddStudent(Student student);
        Student EditStudent(int studentId, Student newStudent);
        Student DeleteStudent(int id);
    }
}
