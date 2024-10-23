using System.Threading.Tasks;

using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using TestingControllersSample.Controllers;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Tests.TestInfrastructure.Attributes;
using TestingControllersSample.Tests.TestInfrastructure.Constants;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;
using TestingControllersSample.ViewModels;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests;

public partial class SessionControllerTests
{
    public class Index
    {
        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnARedirectToIndexHome_When_IdIsNull([Greedy] SessionController controller)
        {
            var result = await controller.Index(id: null);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(ControllerName.Home, redirectToActionResult.ControllerName);
            Assert.Equal(RestAction.Index, redirectToActionResult.ActionName);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnContentWithSessionNotFound_When_SessionIsNotFound(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] SessionController controller)
        {
            brainstormSessionRepository.Setup().ToReturnNoSessions();

            var result = await controller.Index(InvalidSessionId);

            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal(SessionController.ErrorMessage.SessionNotFound, contentResult.Content);
        }

        [Theory]
        [AutoDomainData]
        public async Task Should_ReturnViewResultWithStormSessionViewModel(
            [Frozen] IBrainstormSessionRepository brainstormSessionRepository,
            [Greedy] SessionController controller)
        {
            var brainstormSessions = BrainstormSessionFaker.Generate(3);
            brainstormSessionRepository.Setup().ToReturn(brainstormSessions[0]);

            var result = await controller.Index(brainstormSessions[0].Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<StormSessionViewModel>(viewResult.ViewData.Model);
            Assert.Equal(brainstormSessions[0].Name, model.Name);
            Assert.Equal(brainstormSessions[0].DateCreated.Day, model.DateCreated.Day);
            Assert.Equal(brainstormSessions[0].Id, model.Id);
        }
    }
}
