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
        // class vars for use in all tests
        ApplicationDbContext _context;
        CategoriesController controller;

        // setup method, runs automatically before every test but not a unit test itself. doesn't have to be called
        [TestInitialize]
        public void TestInitialize()
        {
            // mock the db
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

            // instantiate controller w/db dependency for all tests
            controller = new CategoriesController(_context);
        }

        #region "Index"
        [TestMethod]
        public void IndexReturnsView()
        {
            // arrange (now done in TestInitialize)            

            // act 
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsCategories()
        {
            // arrange

            // act 
            var result = (ViewResult)controller.Index().Result;
            var model = (List<Category>)result.Model;

            // assert
            CollectionAssert.AreEqual(_context.Categories.OrderBy(c => c.Name).ToList(), model);
        }
        #endregion

        #region "Details"
        [TestMethod]
        public void DetailsNullIdReturnsErrorView()
        {
            // act => execute w/o any id value
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdReturnsErrorView()
        {
            // act => execute w/any value NOT in mock data in TestInitialize() above
            var result = (ViewResult)controller.Details(934).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdReturnsDetailsView()
        {
            // act => execute w/any id value in mock data in TestInitialize() above
            var result = (ViewResult)controller.Details(742).Result;

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdReturnsCategory()
        {
            // act => execute w/any id value in mock data in TestInitialize() above
            var result = (ViewResult)controller.Details(742).Result;
            var model = (Category)result.Model;

            // assert
            Assert.AreEqual(_context.Categories.Find(742), model);
        }
        #endregion
    }
}
