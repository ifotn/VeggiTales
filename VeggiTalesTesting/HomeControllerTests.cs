using Microsoft.AspNetCore.Mvc;
using VeggiTales.Controllers;

namespace VeggiTalesTesting
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexReturnsView()
        {
            // arrange
            HomeController controller = new HomeController();

            // act => call method & cast result as ViewResult
            var result = (ViewResult)controller.Index();

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void PrivacyReturnsView()
        {
            // arrange
            HomeController controller = new HomeController();

            // act => call method & cast result as ViewResult
            var result = (ViewResult)controller.Privacy();

            // assert
            Assert.AreEqual("Privacy", result.ViewName);
        }
    }
}