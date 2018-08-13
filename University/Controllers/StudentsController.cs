using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Infrastructure;
using University.Models;

//Route data ____ url: "{controller}/{action}/{id}",
namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        // GET: Students
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {//that the view can configuer the column hyperlinks
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? Defines.StudentContr.NAME_DESC : String.Empty;
            ViewBag.DateSortParm = sortOrder == Defines.StudentContr.DATE ? Defines.StudentContr.DATE_DESC: Defines.StudentContr.DATE;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = studentRepository.Students;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
                //contains (содержит ли имя(фамилия) данную строку)
            }
            switch (sortOrder)
            {
                case Defines.StudentContr.NAME_DESC:
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case Defines.StudentContr.DATE:
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case Defines.StudentContr.DATE_DESC:
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
            // adds a page parameter, a current sort order parameter
            // return View(db.Students.ToList());//gets a list of students from the Students entity
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = studentRepository.GetStudent((int)id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // позваляет запретить подделки запросов сайт атак 
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student)//Bind  является одним из способов защиты от чрезмерной передачи данных в сценариях создания
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentRepository.AddStudent(student);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(String.Empty, Defines.EMessage.UNABLE_TO_SAVE_CHANGES);
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = studentRepository.GetStudent((int)id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, [Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Student newStudent=null;
            try
            {
                newStudent = studentRepository.EditStudent((int)id, student);
                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(String.Empty, Defines.EMessage.UNABLE_TO_SAVE_CHANGES);
            }
            return View(newStudent);
        }



        // GET: Students/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = Defines.EMessage.DELETE_FAILED;
            }
            var student = studentRepository.GetStudent((int)id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                studentRepository.DeleteStudent(id);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        studentRepository.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
