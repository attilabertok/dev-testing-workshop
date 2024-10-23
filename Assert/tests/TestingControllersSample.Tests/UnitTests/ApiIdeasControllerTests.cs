using Bogus;
using JetBrains.Annotations;
using TestingControllersSample.Api;
using TestingControllersSample.Tests.TestInfrastructure.Fakers;
using TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;

namespace TestingControllersSample.Tests.UnitTests;

[UsedImplicitly]
public partial class ApiIdeasControllerTests
{
    private static readonly Faker Faker = new();
    private static readonly IdeaFaker IdeaFaker = new();
    private static readonly BrainstormSessionFaker BrainstormSessionFaker = new(IdeaFaker);
    private static readonly int InvalidSessionId = Faker.Random.Number(1, 1024);

    private static class Setup
    {
        public static IdeasControllerSetup The(IdeasController controller)
        {
            return new IdeasControllerSetup(controller);
        }
    }
}
