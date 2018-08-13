using System.Linq;
using System.Web.Mvc;
using University.Models;

namespace University.Infrastructure
{
    public interface IInstructorRepository
    {
        IQueryable<Instructor> Instructors { get; }
        Instructor GetInstructor(int id);
        Instructor AddInstructor(Instructor instructor);
        void EditInstructor(Instructor instructor);
        Instructor DeleteInstructor(int id);
        SelectList GetOfficeAssignments(Instructor instructor);
        SelectList GetOfficeAssignments();

    }
}
