using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleMultiViewMvc.Data;
using SampleMultiViewMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SampleMultiViewMvc.Controllers
{
public class PeopleController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public PeopleController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
        public async Task<IActionResult> Init()
        {
            // ユーザーを作成する
            if (_userManager.Users.Count() == 0)
            {
                await _userManager.CreateAsync(new ApplicationUser() { Email = "admin@mail.com", UserName = "admin@mail.com" });
                await _userManager.CreateAsync(new ApplicationUser() { Email = "noraml@mail.com", UserName = "noraml@mail.com" });
                await _context.SaveChangesAsync();
            }
            // ロールを作成する
            if (_context.Roles.Count() == 0)
            {
                var roles = new RoleStore<IdentityRole>(_context);
                await roles.CreateAsync(new IdentityRole("Normal"));
                await roles.CreateAsync(new IdentityRole("Administrator"));
                await _context.SaveChangesAsync();
            }
            // ユーザーをロールに結び付ける
            if (_context.UserRoles.Count() == 0)
            {
                var admin = _context.Users.First(u => u.UserName == "admin@mail.com");
                var normal = _context.Users.First(u => u.UserName == "normal@mail.com");
                await _userManager.AddToRoleAsync(admin, "Administrator");
                await _userManager.AddToRoleAsync(admin, "Normal");
                await _userManager.AddToRoleAsync(normal, "Normal");
                await _context.SaveChangesAsync();
            }
            return NoContent();
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
        // ログインユーザーが追加可能
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PerfectureId"] = new SelectList(_context.Set<Perfecture>(), "Id", "Id");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        // ログインユーザーが編集可能
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,Blog,Email,EmployeeNo,Hireate,IsAttendance,Name,PerfectureId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

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
        // 管理者が削除可能
[Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
