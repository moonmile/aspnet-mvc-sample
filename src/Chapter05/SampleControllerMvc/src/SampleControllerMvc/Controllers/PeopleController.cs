using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleControllerMvc.Data;
using SampleControllerMvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace SampleControllerMvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
            Perfecture.Initialize(_context);
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Person.Include(p => p.Perfecture);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["PerfectureId"] = new SelectList(_context.Set<Perfecture>(), "Id", "Id");
            // return View();
            /*
            var person = new Person();
            person.Name = "�V�������O";
            person.EmployeeNo = "ABC-1234";
            return View(person);
            */
            bool IsAdmin = false;
            if (IsAdmin)
            {
                return View();
            }
            else
            {
                return View("CreateEasy");
            }
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Age,Blog,Email,EmployeeNo,Hireate,IsAttendance,Name,PerfectureId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PerfectureId"] = new SelectList(_context.Set<Perfecture>(), "Id", "Id", person.PerfectureId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["PerfectureId"] = new SelectList(_context.Set<Perfecture>(), "Id", "Id", person.PerfectureId);
            return View(person);
        }



        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,Blog,Email,EmployeeNo,Hireate,IsAttendance,Name,PerfectureId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }


            var pe = new Person()
            {
                Id = int.Parse(this.Request.Form["id"][0]),
                Name = this.Request.Form["name"],
                Age = int.Parse(this.Request.Form["age"]),
                Hireate = DateTime.Parse(this.Request.Form["hireate"])
            };


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PerfectureId"] = new SelectList(_context.Set<Perfecture>(), "Id", "Id", person.PerfectureId);
            return View(person);
        }

        // GET: People/Delete/5
[Authorize]
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
    if (person == null)
    {
        return NotFound();
    }
    return View(person);
}

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            // var kv = new { id = 100, name = "masuda" };
            // RedirectToAction("Edit", new { id = 100 });
            // return Redirect("/People/Index");
            /// return Redirect("http://microsoft.com/");

            // return RedirectToAction("Index","People");


        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }


[HttpGet]
public IActionResult IndexGet()
{
    return View();
}
[HttpPost]
public IActionResult IndexPost()
{
    return View();
}
    }
}
