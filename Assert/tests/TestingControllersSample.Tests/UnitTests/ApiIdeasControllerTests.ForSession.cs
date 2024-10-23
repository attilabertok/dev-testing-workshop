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
    public class ForSession
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnHttpNotFound_When_SessionIsInvalid(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] IdeasController controller)
        {
            brainstormSessionRepository.Setup().ToReturnNoSessions();

            var result = await controller.ForSession(InvalidSessionId);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(InvalidSessionId, notFoundObjectResult.Value);
        }

        [Theory]
        [AutoDomainData]
        public async Task ForSession_ReturnsIdeasForSession(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] IdeasController controller)
        {
            var brainstormSession = BrainstormSessionFaker.Generate();
            brainstormSessionRepository.Setup().ToReturn(brainstormSession);

            var result = await controller.ForSession(brainstormSession.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<IdeaDto>>(okResult.Value);
            var idea = returnValue.FirstOrDefault();
            Assert.Equal(brainstormSession.Ideas.FirstOrDefault()?.Name, idea?.Name);
        }
    }
}
