using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestingControllersSample.Api;
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
    public class CreateActionResult
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnBadRequest_When_ModelIsInvalid([Greedy] IdeasController controller)
        {
            Setup.The(controller).StateToError();

            var result = await controller.CreateActionResult(model: null);

            var actionResult = Assert.IsType<ActionResult<BrainstormSession>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnNotFoundObjectResult_WhenSessionDoesNotExist([Greedy] IdeasController controller)
        {
            var newIdea = IdeaFaker.Generate().ToNewIdeaModel();

            var result = await controller.CreateActionResult(newIdea);

            var actionResult = Assert.IsType<ActionResult<BrainstormSession>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
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
            var newIdea = IdeaFaker.Generate().ToNewIdeaModel().WithSessionId(brainstormSession.Id);
            var originalIdeaCount = brainstormSession.Ideas.Count;

            var result = await controller.CreateActionResult(newIdea);

            var actionResult = Assert.IsType<ActionResult<BrainstormSession>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<BrainstormSession>(createdAtActionResult.Value);
            await brainstormSessionRepository.Received().UpdateAsync(brainstormSession);
            Assert.Equal(originalIdeaCount + 1, returnValue.Ideas.Count);
            Assert.Equal(newIdea.Name, returnValue.Ideas.LastOrDefault()!.Name);
            Assert.Equal(newIdea.Description, returnValue.Ideas.LastOrDefault()!.Description);
        }
    }
}
