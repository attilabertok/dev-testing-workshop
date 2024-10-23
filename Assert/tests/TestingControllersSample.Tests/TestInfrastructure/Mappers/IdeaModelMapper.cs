using TestingControllersSample.ClientModels;
using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Tests.TestInfrastructure.Mappers;

public static class IdeaModelMapper
{
    public static NewIdeaModel ToNewIdeaModel(this Idea idea)
    {
        return new NewIdeaModel
        {
            Description = idea.Description,
            Name = idea.Name
        };
    }

    public static Idea ToIdea(this NewIdeaModel model)
    {
        return new Idea
        {
            Id = model.SessionId,
            Description = model.Description,
            Name = model.Name
        };
    }
}
