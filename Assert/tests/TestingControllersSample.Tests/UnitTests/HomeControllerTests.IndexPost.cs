using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestingControllersSample.Controllers;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Core.Model;
using TestingControllersSample.Tests.TestInfrastructure.Attributes;
using TestingControllersSample.Tests.TestInfrastructure.Constants;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class HomeControllerTests
{
    public class IndexPost
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnBadRequestResult_When_ModelStateIsInvalid(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] HomeController controller)
        {
            var sessions = BrainstormSessionFaker.Generate(3);
            brainstormSessionRepository.Setup().ToList(sessions);
            Setup.The(controller).StateToError();
            var newSession = new NewSessionModel();

            var result = await controller.Index(newSession);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnARedirectAndAddSession_When_ModelStateIsValid(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] HomeController controller)
        {
            brainstormSessionRepository.Setup().ToAddSuccessfully();
            var newSession = NewSessionModelFaker.Generate();

            var result = await controller.Index(newSession);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal(RestAction.Index, redirectToActionResult.ActionName);
            await brainstormSessionRepository.Received().AddAsync(Arg.Any<BrainstormSession>());
        }
    }
}
