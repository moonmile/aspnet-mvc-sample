using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleTestControllerMvc.Controllers;
using Xunit;

namespace SampleTestControllerMvc.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void TestHomeController_Index()
        {
            var controller = new HomeController();
            // Act
            var result = controller.Index();
            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void TestHomeController_About()
        {
            var controller = new HomeController();
            // Act
            var result = controller.About();
            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal("Your application description page.", view.ViewData["Message"]);
        }
    }
}
