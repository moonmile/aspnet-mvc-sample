using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleRoutingMvc.Data;
using SampleRoutingMvc.Models;

namespace SampleRoutingMvc.Controllers
{
[Produces("application/json")]
[Route("api/People")]
public class PeopleApiController : Controller
{
    private readonly ApplicationDbContext _context;

    public PeopleApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/People
    [HttpGet]
    public IEnumerable<Person> GetPerson()
    {
        return _context.Person;
    }

    // GET: api/People
    [HttpGet]
    [Route("search/name/{value}")]
    public IEnumerable<Person> GetPersonByName([FromRoute] string value )
    {
        return _context.Person.Where( t => t.Name.Contains(value));
    }
    // GET: api/People
    [HttpGet]
    [Route("search/age/{value}")]
    public IEnumerable<Person> GetPersonByAge([FromRoute] int value)
    {
        return _context.Person.Where(t => t.Age >= value);
    }


    // GET: api/People/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Person person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    // PUT: api/People/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != person.Id)
        {
            return BadRequest();
        }

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        // return NoContent();
    }

    // POST: api/People
    [HttpPost]
    public async Task<IActionResult> PostPerson([FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Person.Add(person);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (PersonExists(person.Id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] Person person)
    {
        return await PostPerson(person);
    }

    [HttpPost]
    [Route("Update/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Person person)
    {
        return await PutPerson(id, person);
    }




    // DELETE: api/People/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Person person = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
        if (person == null)
        {
            return NotFound();
        }

        _context.Person.Remove(person);
        await _context.SaveChangesAsync();

        return Ok(person);
    }

    private bool PersonExists(int id)
    {
        return _context.Person.Any(e => e.Id == id);
    }
}
}