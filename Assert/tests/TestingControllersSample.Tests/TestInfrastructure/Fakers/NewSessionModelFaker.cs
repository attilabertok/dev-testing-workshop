using Bogus;
using TestingControllersSample.Controllers;

namespace TestingControllersSample.Tests.TestInfrastructure.Fakers;

public sealed class NewSessionModelFaker : Faker<NewSessionModel>
{
    public NewSessionModelFaker()
    {
        RuleFor(m => m.SessionName, f => f.Hacker.Phrase());
    }
}
