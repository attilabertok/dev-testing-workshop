using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace TestingControllersSample.Tests.TestInfrastructure.Attributes;

public class AutoDomainDataAttribute() : AutoDataAttribute(
    () => new Fixture()
        .Customize(new AutoNSubstituteCustomization()));
