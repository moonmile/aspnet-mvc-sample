using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleTestControllerMvc.Controllers;
using Xunit;
using SampleTestControllerMvc.Data;
using Microsoft.EntityFrameworkCore;
using SampleTestControllerMvc.Models;

namespace SampleTestControllerMvc.Tests
{
public class PeopleControllerTests
{
    string DefaultConnection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-SampleTestControllerMvc-81ab55bb-da15-4b73-95c0-8d0453f9b521;Trusted_Connection=True;MultipleActiveResultSets=true";
    [Fact]
    public async void TestIndex()
    {
        var opb = new DbContextOptionsBuilder<ApplicationDbContext>();
        opb.UseSqlServer(DefaultConnection);
        var dc = new ApplicationDbContext(opb.Options);

        var controller = new PeopleController(dc);
        // Act
        var result = await controller.Index();
        // Assert
        Assert.NotNull(result);
        var view = Assert.IsType<ViewResult>(result);
        Assert.NotNull(view.Model);
        var model = Assert.IsType<List<Person>>(view.Model);
        Assert.Equal(1, model.Count);
        var person = model[0];
        Assert.Equal("masuda", person.Name);
        Assert.Equal(48, person.Age);
    }
}
}
