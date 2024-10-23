using Bogus;
using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Tests.TestInfrastructure.Fakers;

public sealed class IdeaFaker : Faker<Idea>
{
    public IdeaFaker()
    {
        RuleFor(idea => idea.Id, f => f.Random.Number(0, 1024));
        RuleFor(idea => idea.Name, f => f.Commerce.ProductName());
        RuleFor(idea => idea.Description, f => f.Commerce.ProductDescription());
        RuleFor(idea => idea.DateCreated, f => f.Date.Recent());
    }
}
