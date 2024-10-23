using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestingControllersSample.Api;
using TestingControllersSample.ClientModels;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Core.Model;
using TestingControllersSample.Tests.TestInfrastructure.Attributes;
using TestingControllersSample.Tests.TestInfrastructure.Extensions;
using TestingControllersSample.Tests.TestInfrastructure.Mappers;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class ApiIdeasControllerTests
{
    public class Create
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnBadRequest_WhenModelIsInvalid([Greedy] IdeasController controller)
        {
            Setup.The(controller).StateToError();

            var result = await controller.Create(model: null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnHttpNotFound_When_SessionIsInvalid(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] IdeasController controller)
        {
            brainstormSessionRepository.Setup().ToReturnNoSessions();

            var result = await controller.Create(new NewIdeaModel());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnNewlyCreatedIdeaForSession(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] IdeasController controller)
        {
            var brainstormSession = BrainstormSessionFaker.Generate();
            brainstormSessionRepository.Setup().ToReturn(brainstormSession);
            brainstormSessionRepository.Setup().ToUpdateSuccessfully(brainstormSession);
            var newIdea = IdeaFaker.Generate();
            var model = newIdea.ToNewIdeaModel().WithSessionId(brainstormSession.Id);
            var originalIdeaCount = brainstormSession.Ideas.Count;

            var result = await controller.Create(model);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSession = Assert.IsType<BrainstormSession>(okResult.Value);
            await brainstormSessionRepository.Received().UpdateAsync(brainstormSession);
            Assert.Equal(originalIdeaCount + 1, returnSession.Ideas.Count);
            Assert.Equal(newIdea.Name, returnSession.Ideas.LastOrDefault()!.Name);
            Assert.Equal(newIdea.Description, returnSession.Ideas.LastOrDefault()!.Description);
        }
    }
}
