using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using University.DAL;
using University.Infrastructure;
using University.Models;

namespace University.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext schoolContext;
        
        public IQueryable<Course> Courses { get => schoolContext.Courses; }

        public CourseRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }
        public Course GetCourse(int id)
        {
            return schoolContext.Courses.Find(id);
        }
        public Course AddCourse(Course course)
        {
            var addedCourse = schoolContext.Courses.Add(course);
            schoolContext.SaveChanges();
            return addedCourse;
        }

        public Course DeleteCourse(int id)
        {
            var course = schoolContext.Courses.Find(id);
            schoolContext.Courses.Remove(course);
            schoolContext.SaveChanges();
            return course;
        }

        public void EditCourse( Course newCourse)
        {
            schoolContext.Entry(newCourse).State = EntityState.Modified;
            schoolContext.SaveChanges();
        }

        public SelectList GetDepartments(Course newCourse)
        {
            return new SelectList(schoolContext.Departments, "DepartmentID", "Name", newCourse.DepartmentID);
        }
        public SelectList GetDepartments()
        {
            return new SelectList(schoolContext.Departments, "DepartmentID", "Name");
        }
    }
}