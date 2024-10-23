using Bogus;
using TestingControllersSample.Controllers;

namespace TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;

internal class SessionControllerSetup(SessionController controller)
{
    private static readonly Faker Faker = new();

    public void StateToError()
    {
        controller.ModelState.AddModelError(Faker.Lorem.Word(), Faker.Lorem.Sentence());
    }
}
