using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeitWorkshop.Data;
using FeitWorkshop.Models;
using FeitWorkshop.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FeitWorkshop.Controllers
{
    public class CoursesController : Controller
    {
        private readonly FeitWorkshopContext _context;

        public CoursesController(FeitWorkshopContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int CourseSemester, string searchString, string CourseProgram)
        {
            /*var feitWorkshopContext = _context.Courses.Include(c => c.FirstTeacher).Include(c => c.SecondTeacher)
                .Include(m => m.Students).ThenInclude(m => m.Student);
            return View(await feitWorkshopContext.ToListAsync());*/

            IQueryable<Course> courses = _context.Courses.AsQueryable();
            IQueryable<int> semesterQuery = _context.Courses.OrderBy(m => m.Semester).Select(m => m.Semester).Distinct();
            IQueryable<string> programQuery = _context.Courses.OrderBy(m => m.Programme).Select(m => m.Programme).Distinct();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Title.Contains(searchString));
            }
            if (CourseSemester > 0)
            {
                courses = courses.Where(x => x.Semester == CourseSemester);
            }
            if (!string.IsNullOrEmpty(CourseProgram))
            {
                courses = courses.Where(x => x.Programme == CourseProgram);
            }

            courses = courses.Include(c => c.FirstTeacher).Include(c => c.SecondTeacher)
                .Include(m => m.Students).ThenInclude(m => m.Student);

            var coursesVM = new CourseVM
            {
                Semester = new SelectList(await semesterQuery.ToListAsync()),
                Program = new SelectList(await programQuery.ToListAsync()),
                Courses = await courses.ToListAsync()
            };

            return View(coursesVM);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher)
                .Include(m => m.Students).ThenInclude(m => m.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["FirstTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName");
            ViewData["SecondTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Credits,Semester,Programme,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", course.SecondTeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Courses.Where(m => m.Id == id).Include(m => m.Students).First();


            if (course == null)
            {
                return NotFound();
            }

            var students = _context.Students.AsEnumerable();
            students = students.OrderBy(s => s.FullName);

            EnrollmentsVM viewmodel = new EnrollmentsVM
            {
                Course = course,
                StudentList = new MultiSelectList(students, "Id", "FullName"),
                SelectedStudents = course.Students.Select(sa => sa.StudentId)
            };

            ViewData["FirstTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", course.SecondTeacherId);
            return View(viewmodel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, EnrollmentsVM viewmodel)
        {
            if (id != viewmodel.Course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Course);
                    await _context.SaveChangesAsync();

                    IEnumerable<int?> listStudents = viewmodel.SelectedStudents;
                    IQueryable<Enrollment> toBeRemoved = _context.Enrollments.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                    _context.Enrollments.RemoveRange(toBeRemoved);

                    IEnumerable<int?> existStudents = _context.Enrollments.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                    IEnumerable<int?> newStudents = listStudents.Where(s => !existStudents.Contains(s));
                    foreach (int studentId in newStudents)
                    {
                        _context.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = id });
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewmodel.Course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", viewmodel.Course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", viewmodel.Course.SecondTeacherId);
            return View(viewmodel);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher)
                .Include(m => m.Students).ThenInclude(m => m.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
