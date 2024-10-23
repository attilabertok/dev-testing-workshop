using Bogus;
using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Tests.TestInfrastructure.Fakers;

public sealed class BrainstormSessionFaker : Faker<BrainstormSession>
{
    public BrainstormSessionFaker(IdeaFaker ideaFaker)
    {
        RuleFor(brainstormSession => brainstormSession.Id, f => f.Random.Number(0, 1024));
        RuleFor(brainstormSession => brainstormSession.Name, f => f.Hacker.Noun());
        RuleFor(brainstormSession => brainstormSession.DateCreated, f => f.Date.Recent());
        FinishWith((f, brainstormSession) => ideaFaker.Generate(f.Random.Number(0, 3)).ForEach(brainstormSession.AddIdea));
    }
}
