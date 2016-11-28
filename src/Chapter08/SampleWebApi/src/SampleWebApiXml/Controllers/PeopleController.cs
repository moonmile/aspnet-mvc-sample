using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiXml.Data;
using SampleWebApiXml.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApiXml.Controllers
{
[Route("api/[controller]")]
public class PeopleController : Controller
{
    private readonly ApplicationDbContext _context;

    public PeopleController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/values
    [HttpGet]
    public async Task<People> Get()
    {
        Perfecture.Initialize(_context);
        var applicationDbContext = _context.Person.Include(p => p.Perfecture);
        return new People
        {
            Items = await applicationDbContext.ToListAsync()
        };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<Person> Get(int? id)
    {
        if (id == null)
        {
            return null;
        }

        var person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
        if (person == null)
        {
            return null;
        }
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
