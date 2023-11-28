using Microsoft.AspNetCore.Mvc;
using VeggiTales.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggiTales.Data;
using Microsoft.EntityFrameworkCore;
using VeggiTales.Models;

namespace VeggiTalesTesting
{
    [TestClass]
    public class CategoriesControllerTests
    {

        [TestMethod]
        public void IndexReturnsView()
        {
            // arrange
            // mock db
            ApplicationDbContext _context;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            CategoriesController controller = new CategoriesController(_context);

            // act 
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsCategories()
        {
            // arrange
            // mock db
            ApplicationDbContext _context;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // add data to in-memory db
            var category = new Category { CategoryId = 289, Name = "Some Category" };
            _context.Categories.Add(category);

            category = new Category { CategoryId = 742, Name = "Another Category" };
            _context.Categories.Add(category);

            category = new Category { CategoryId = 935, Name = "Last Category" };
            _context.Categories.Add(category);

            _context.SaveChanges();

            CategoriesController controller = new CategoriesController(_context);

            // act 
            var result = (ViewResult)controller.Index().Result;
            var model = (List<Category>)result.Model;

            // assert
            CollectionAssert.AreEqual(_context.Categories.ToList(), model);
        }
    }
}
