using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using University.Models;

namespace University.Infrastructure
{
    public interface ICourseRepository
    {
        IQueryable<Course> Courses { get; }
        Course GetCourse(int id);
        Course AddCourse(Course course);
        void EditCourse( Course newCourse);
        SelectList GetDepartments(Course newCourse);
        SelectList GetDepartments();
        Course DeleteCourse(int id);
    }
}
