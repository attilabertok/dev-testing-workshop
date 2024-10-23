using Bogus;
using JetBrains.Annotations;
using TestingControllersSample.Tests.TestInfrastructure.Fakers;

namespace TestingControllersSample.Tests.UnitTests;

[UsedImplicitly]
public partial class SessionControllerTests
{
    private static readonly Faker Faker = new();
    private static readonly int InvalidSessionId = Faker.Random.Number(1, 1024);
    private static readonly IdeaFaker IdeaFaker = new();
    private static readonly BrainstormSessionFaker BrainstormSessionFaker = new(IdeaFaker);
}
