using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Utils;
using System.Collections;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace eWeb.Controllers
{
    public class CoursesController : BaseController
    {
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var list = await ApiHandler.DeserializeApiResponse<IEnumerable<Course>>(_CoursesURL, HttpMethod.Get);
            return list != null ? 
                    View(list) :
                    Problem("Entity set 'ApplicationDBContext.Courses'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);    
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,Credit")] Course course)
        {
            if (ModelState.IsValid)
            {
                await ApiHandler.DeserializeApiResponse<Course>(_CoursesURL, HttpMethod.Post, course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,Credit")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var temp = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);
                    if (temp == null) return NotFound();
                    await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Put, course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseExists(course.CourseId))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);

            if (course != null)
            {
                await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Delete);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CourseExists(int id)
        {
            var course = await ApiHandler.DeserializeApiResponse<Course>($"{_CoursesURL}/{id}", HttpMethod.Get);
            return course != null;
        }
    }
}
