using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Data;
using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(
            ApplicationDbContext context)
        {
            _context = context;
            // 初回のみ都道府県のデータを作る
            Prefecture.Initialize(_context);
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var applicationDbContext = _context.Person.Include(p => p.Prefecture);
            return await applicationDbContext.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Person> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var person = await _context.Person
                        .Include(p => p.Prefecture)
                        .SingleOrDefaultAsync(m => m.Id == id);
            return person;
        }

        // POST api/values
        [HttpPost]
        public async Task<int> Post([FromBody]Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]Person person)
        {
            if (id != person.Id)
            {
                return -1;
            }
            _context.Update(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
