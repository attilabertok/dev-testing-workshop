using System.Collections.Generic;
using System.Threading.Tasks;

using NSubstitute;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;

internal static class BrainstormSessionRepositoryExtensions
{
    internal static RepositorySetup Setup(this IBrainstormSessionRepository brainstormSessionRepository)
    {
        return new RepositorySetup(brainstormSessionRepository);
    }

    internal class RepositorySetup(IBrainstormSessionRepository brainstormSessions)
    {
        public void ToReturnNoSessions()
        {
            brainstormSessions.GetByIdAsync(Arg.Any<int>()).Returns((BrainstormSession)null);
        }

        public void ToReturn(BrainstormSession session)
        {
            brainstormSessions.GetByIdAsync(session.Id).Returns(session);
        }

        public void ToAddSuccessfully()
        {
            brainstormSessions.AddAsync(Arg.Any<BrainstormSession>()).Returns(Task.CompletedTask);
        }

        public void ToUpdateSuccessfully(BrainstormSession session)
        {
            brainstormSessions.UpdateAsync(session).Returns(Task.CompletedTask);
        }

        public void ToList(List<BrainstormSession> sessions)
        {
            brainstormSessions.ListAsync().Returns(sessions);
        }
    }
}
