using System;

using TestingControllersSample.Controllers;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class HomeControllerTests
{
    public class Constructor
    {
        [Fact]
        public void GivenNullSessionRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateSut);
        }

        private static HomeController CreateSut()
        {
            return new HomeController(null!);
        }
    }
}
