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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FeitWorkshop.Controllers
{
    public class StudentsController : Controller
    {
        private readonly FeitWorkshopContext _context;
        private readonly IHostingEnvironment webHostEnvironment;

        public StudentsController(FeitWorkshopContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Students
        public async Task<IActionResult> Index(string FName, string LName, string Id)
        {

            IQueryable<Student> students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(FName))
            {
                students = students.Where(s => s.FirstName.Contains(FName));
            }
            if (!string.IsNullOrEmpty(LName))
            {
                students = students.Where(s => s.LastName.Contains(LName));
            }
            if (!string.IsNullOrEmpty(Id))
            {
                students = students.Where(s => s.StudentId.Contains(Id));
            }

            var studentsVM = new StudentVM
            {
                Studens = await students.ToListAsync()
            };

            return View(studentsVM);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(m => m.Courses).ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(StudentVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Student student = new Student
                {
                    StudentId = model.StudentId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EnrollmentDate = model.EnrollmentDate,
                    AcquiredCredits = model.AcquiredCredits,
                    CurrentSemestar = model.CurrentSemestar,
                    EducationLevel = model.EducationLevel,
                    email = model.email,
                    password = model.password,
                    ProfilePicture = uniqueFileName,
                };


                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private string UploadedFile(StudentVM model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            StudentVM viewmodel = new StudentVM
            {
                Student = student,
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
                AcquiredCredits = student.AcquiredCredits,
                CurrentSemestar = student.CurrentSemestar,
                EducationLevel = student.EducationLevel,
                email = student.email,
                password = student.password
            };
            return View(viewmodel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, StudentVM viewmodel)
        {
            
            if (id != viewmodel.Student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(viewmodel);

                    viewmodel.Student.StudentId = viewmodel.StudentId;
                    viewmodel.Student.FirstName = viewmodel.FirstName;
                    viewmodel.Student.LastName = viewmodel.LastName;
                    viewmodel.Student.EnrollmentDate = viewmodel.EnrollmentDate;
                    viewmodel.Student.AcquiredCredits = viewmodel.AcquiredCredits;
                    viewmodel.Student.CurrentSemestar = viewmodel.CurrentSemestar;
                    viewmodel.Student.EducationLevel = viewmodel.EducationLevel;
                    viewmodel.Student.email = viewmodel.email;
                    viewmodel.Student.password = viewmodel.password;

                    viewmodel.Student.ProfilePicture = uniqueFileName;
                    _context.Update(viewmodel.Student);
                    await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewmodel.Student.Id))
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
            return View(viewmodel);
            
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(m => m.Courses).ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(m => m.Courses).ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

    }
}
