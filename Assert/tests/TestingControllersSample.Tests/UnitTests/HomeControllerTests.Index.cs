using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using TestingControllersSample.Controllers;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Tests.TestInfrastructure.Attributes;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;
using TestingControllersSample.ViewModels;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class HomeControllerTests
{
    public class Index
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnAViewResultWithAListOfBrainstormSessions(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] HomeController controller)
        {
            var sessions = BrainstormSessionFaker.Generate(3);
            brainstormSessionRepository.Setup().ToList(sessions);
            var originalModelCount = sessions.Count;

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(originalModelCount, model.Count());
        }
    }
}
