﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Data;
using SampleWebApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApi.Controllers
{
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
    public IEnumerable<Perfecture> Get()
    {
        return _context.Perfecture.ToList();
    }
}
}
