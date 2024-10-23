using JetBrains.Annotations;
using TestingControllersSample.Controllers;
using TestingControllersSample.Tests.TestInfrastructure.Fakers;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;

namespace TestingControllersSample.Tests.UnitTests;

[UsedImplicitly]
public partial class HomeControllerTests
{
    private static readonly IdeaFaker IdeaFaker = new();
    private static readonly BrainstormSessionFaker BrainstormSessionFaker = new(IdeaFaker);
    private static readonly NewSessionModelFaker NewSessionModelFaker = new();

    private static class Setup
    {
        public static HomeControllerSetup The(HomeController controller)
        {
            return new HomeControllerSetup(controller);
        }
    }
}