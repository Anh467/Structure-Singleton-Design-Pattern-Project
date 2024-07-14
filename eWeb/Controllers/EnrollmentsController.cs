using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eBookStore.Utils;
using BusinessObject;

namespace eWeb.Controllers
{
    public class EnrollmentsController : BaseController
    {
        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var list = await ApiHandler.DeserializeApiResponse<IEnumerable<Enrollment>>(_EnrollmentsURL, HttpMethod.Get);
            return View(list);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? studentid, int? courseid)
        {
            if (studentid == null || courseid == null)
            {
                return NotFound();
            }

            var enrollment = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public async Task<IActionResult> Create()
        {
            var courses = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
            var students = await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
            ViewData["CourseId"] = new SelectList(courses, "CourseID", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "StudentID", "StudentName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,EnrollmentDate")] Enrollment enrollment)
        {
            var temp = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{enrollment.StudentId}/{enrollment.CourseId}", HttpMethod.Get);
            if(temp != null)
            {
                var coursest = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
                var studentst= await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
                ViewData["CourseId"] = new SelectList(coursest, "CourseID", "CourseName");
                ViewData["StudentId"] = new SelectList(studentst, "StudentID", "StudentName");
                ViewBag.Message = "Course and student id already exist";
                return View(enrollment);
            }

            if (ModelState.IsValid)
            {
                await ApiHandler.DeserializeApiResponse<Enrollment>(_EnrollmentsURL, HttpMethod.Post, enrollment);
                return RedirectToAction(nameof(Index));
            }

            var courses = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
            var students = await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
            ViewData["CourseId"] = new SelectList(courses, "CourseID", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "StudentID", "StudentName");
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? studentid, int? courseid)
        {
            if (studentid == null || courseid == null)
            {
                return NotFound();
            }

            var enrollment = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);

            if (enrollment == null)
            {
                return NotFound();
            }

            var courses = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
            var students = await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
            ViewData["CourseId"] = new SelectList(courses, "CourseID", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "StudentID", "Address");
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int studentid, int courseid, [Bind("StudentId,CourseId,EnrollmentDate")] Enrollment enrollment)
        {
            if (studentid != enrollment.StudentId || courseid != enrollment.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var temp = ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);
                    if(temp == null) return NotFound();

                    await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Put, enrollment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EnrollmentExists(enrollment.StudentId, enrollment.CourseId))
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

            var courses = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
            var students = await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
            ViewData["CourseId"] = new SelectList(courses, "CourseID", "CourseName");
            ViewData["StudentId"] = new SelectList(students, "StudentID", "StudentName");
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? studentid, int? courseid)
        {
            if (studentid == null || courseid == null)
            {
                return NotFound();
            }

            var enrollment = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentid, int courseid)
        {
            var enrollment = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);
            if (enrollment != null)
            {
                await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Delete);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EnrollmentExists(int studentid, int courseid)
        {
            var enrollment = await ApiHandler.DeserializeApiResponse<Enrollment>($"{_EnrollmentsURL}/{studentid}/{courseid}", HttpMethod.Get);
            return enrollment != null;
        }
    }
}
