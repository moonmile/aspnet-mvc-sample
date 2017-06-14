using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleListDetailMvc.Data;
using SampleListDetailMvc.Models;

namespace SampleListDetailMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
            Prefecture.Initialize(_context);
        }

        // GET: Books
#if true
public async Task<IActionResult> Index(int? page, string search)
{
    if (page == null)
    {
        page = 0;
    }
    int max = 5;

    var books = from m in _context.Book select m;
    if (!string.IsNullOrEmpty(search))
    {
        books = books.Where(b => b.Title.Contains(search));
    }
    books = books
        .Skip(max * page.Value).Take(max)
        .Include(b => b.Author).Include(b => b.Publisher);

    if (page.Value > 0)
    {
        ViewData["prev"] = page.Value - 1;
    }
    if (books.Count() >= max)
    {
        ViewData["next"] = page.Value + 1;
        if (_context.Book.Skip(max * (page.Value + 1)).Take(max).Count() == 0)
        {
            ViewData["next"] = null;
        }
    }
    ViewData["search"] = search;
    return View(await books.ToListAsync());
}
#endif
#if false
        // 元のIndexメソッド
        public async Task<IActionResult> Index(int? page, string search)
        {
            var books = _context.Book.Include(b => b.Author);
            return View(await books.ToListAsync());
        }
#endif
#if false
        // ページングのみ実装
public async Task<IActionResult> Index(int? page)
{
    // ページング機能
    if (page == null)
    {
        page = 0;
    }
    int max = 5;

    var books = _context.Book
        .Skip(max * page.Value).Take(max)
        .Include(b => b.Author).Include(b => b.Publisher)
        .OrderBy( b => b.Id )
        ;

    if (page.Value > 0)
    {
        ViewData["prev"] = page.Value - 1;
    }
    if (books.Count() >= max)
    {
        ViewData["next"] = page.Value + 1;
        // 次のページがあるか調べる
        if ( _context.Book.Skip(max * (page.Value+1)).Take(max).Count() == 0 )
        {
            ViewData["next"] = null;
        }
    }
    return View(await books.ToListAsync());
}
#endif

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Set<Publisher>(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,ISBN,Price,PublishDate,PublisherId,Title")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["PublisherId"] = new SelectList(_context.Set<Publisher>(), "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["PublisherId"] = new SelectList(_context.Set<Publisher>(), "Id", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorId,ISBN,Price,PublishDate,PublisherId,Title")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["PublisherId"] = new SelectList(_context.Set<Publisher>(), "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.SingleOrDefaultAsync(m => m.Id == id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
