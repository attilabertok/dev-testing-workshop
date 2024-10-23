using TestingControllersSample.ClientModels;

namespace TestingControllersSample.Tests.TestInfrastructure.Extensions;

public static class NewIdeaModelExtensions
{
    public static NewIdeaModel WithSessionId(this NewIdeaModel instance, int sessionId)
    {
        instance.SessionId = sessionId;

        return instance;
    }
}
