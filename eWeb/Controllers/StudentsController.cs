using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using eBookStore.Utils;

namespace eWeb.Controllers
{
    public class StudentsController : BaseController
    {
        // GET: Students
        public async Task<IActionResult> Index()
        {
            var list = await ApiHandler.DeserializeApiResponse<IEnumerable<Student>>(_StudentUrl, HttpMethod.Get);
            return list != null ? 
                View(list) :
                Problem("Entity set 'ApplicationDBContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Get);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                await ApiHandler.DeserializeApiResponse<Student>(_StudentUrl, HttpMethod.Post, student); 
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Get);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,Address")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var temp = await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Get);
                    if (temp == null)
                    {
                        NotFound();
                    }

                    await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Put, student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await StudentExists(student.StudentId))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Get);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Get);

            if (student != null)
            {
                await ApiHandler.DeserializeApiResponse<Student>($"{_StudentUrl}/{id}", HttpMethod.Delete);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentExists(int id)
        {
            var student = await ApiHandler.DeserializeApiResponse<Student>(_StudentUrl, HttpMethod.Get);
            return student != null;
        }
    }
}
