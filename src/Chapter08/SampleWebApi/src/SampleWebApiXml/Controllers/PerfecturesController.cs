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
public class PrefecturesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PrefecturesController(
        ApplicationDbContext context)
    {
        _context = context;
            
    }

    // GET: api/values
    [HttpGet]
    public Prefectures Get()
    {
        return new Prefectures() { Items = _context.Prefecture.ToList() };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Prefecture Get(int id)
    {
        return _context.Prefecture.FirstOrDefault(m => m.Id == id);
    }
}
}
