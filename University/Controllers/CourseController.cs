using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Infrastructure;
using University.Models;

namespace University.Controllers
{
    public class CourseController : Controller
    {
      // private SchoolContext db = new SchoolContext();
        private readonly ICourseRepository courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        // GET: Course
        public ActionResult Index()
        {
            var courses = courseRepository.Courses.Include(c => c.Department);
            //безотложная загрузка свойства навигации
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = courseRepository.GetCourse((int)id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = courseRepository.GetDepartments();
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
              courseRepository.AddCourse(course);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = courseRepository.GetDepartments(course);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = courseRepository.GetCourse((int)id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = courseRepository.GetDepartments(course);

            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
                courseRepository.EditCourse(course);
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = courseRepository.GetDepartments(course);
            return View(course);
        }


        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = courseRepository.GetCourse((int)id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = courseRepository.GetCourse((int)id);
            courseRepository.DeleteCourse(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}