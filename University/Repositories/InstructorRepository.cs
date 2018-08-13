using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using University.DAL;
using University.Infrastructure;
using University.Models;

namespace University.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolContext schoolContext;

        public IQueryable<Instructor> Instructors{ get => schoolContext.Instructors; }

        public InstructorRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }
        public Instructor AddInstructor(Instructor instructor)
        {
            var addedInstructor = schoolContext.Instructors.Add(instructor);
            schoolContext.SaveChanges();
            return addedInstructor;
        }

        public Instructor DeleteInstructor(int id)
        {
            var instructor = schoolContext.Instructors.Find(id);
            schoolContext.Instructors.Remove(instructor);
            schoolContext.SaveChanges();
            return instructor;
        }

        public void EditInstructor(Instructor instructor)
        {
            schoolContext.Entry(instructor).State = EntityState.Modified;
            schoolContext.SaveChanges();
        
        }

        public Instructor GetInstructor(int id)
        {
            return schoolContext.Instructors.Find(id);
        }
        public SelectList GetOfficeAssignments(Instructor instructor)
        {
            return new SelectList(schoolContext.OfficeAssignments, "InstructorID", "Location", instructor.ID);
        }
        public SelectList GetOfficeAssignments()
        {
            return new SelectList(schoolContext.OfficeAssignments, "InstructorID", "Location");
        }
    }
}