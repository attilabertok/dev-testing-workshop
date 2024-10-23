using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using TestingControllersSample.Api;
using TestingControllersSample.ClientModels;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Tests.TestInfrastructure.Attributes;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class ApiIdeasControllerTests
{
    public class ForSessionActionResult
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnNotFoundObjectResult_When_SessionDoesNotExist(
            [Greedy] IdeasController controller)
        {
            var result = await controller.ForSessionActionResult(InvalidSessionId);

            var actionResult = Assert.IsType<ActionResult<List<IdeaDto>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnIdeasForSession(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] IdeasController controller)
        {
            var brainstormSession = BrainstormSessionFaker.Generate();
            brainstormSessionRepository.Setup().ToReturn(brainstormSession);

            var result = await controller.ForSessionActionResult(brainstormSession.Id);

            var actionResult = Assert.IsType<ActionResult<List<IdeaDto>>>(result);
            var returnValue = Assert.IsType<List<IdeaDto>>(actionResult.Value);
            var idea = returnValue.FirstOrDefault();
            Assert.Equal(brainstormSession.Ideas.FirstOrDefault()?.Name, idea?.Name);
        }
    }
}
