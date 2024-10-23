using Bogus;
using TestingControllersSample.Api;

namespace TestingControllersSample.Tests.TestInfrastructure.SetupHelpers;

internal class IdeasControllerSetup(IdeasController controller)
{
    private static readonly Faker Faker = new();

    public void StateToError()
    {
        controller.ModelState.AddModelError(Faker.Lorem.Word(), Faker.Lorem.Sentence());
    }
}
