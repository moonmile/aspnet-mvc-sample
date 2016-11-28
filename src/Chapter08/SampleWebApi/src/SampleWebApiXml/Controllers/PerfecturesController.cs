using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiXml.Data;
using SampleWebApiXml.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApiXml.Controllers
{
[Produces("application/xml")]
[Route("api/[controller]")]
public class PerfecturesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PerfecturesController(
        ApplicationDbContext context)
    {
        _context = context;
            
    }

    // GET: api/values
    [HttpGet]
    public Perfectures Get()
    {
        return new Perfectures() { Items = _context.Perfecture.ToList() };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Perfecture Get(int id)
    {
        return _context.Perfecture.FirstOrDefault(m => m.Id == id);
    }
}
}
