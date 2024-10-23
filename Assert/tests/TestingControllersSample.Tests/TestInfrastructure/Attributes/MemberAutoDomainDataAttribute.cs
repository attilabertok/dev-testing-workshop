using System;

using AutoFixture;
using AutoFixture.AutoNSubstitute;

using Objectivity.AutoFixture.XUnit2.Core.Attributes;
using Objectivity.AutoFixture.XUnit2.Core.Common;
using Objectivity.AutoFixture.XUnit2.Core.Providers;

using Xunit.Sdk;

namespace TestingControllersSample.Tests.TestInfrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[DataDiscoverer("AutoFixture.Xunit2.NoPreDiscoveryDataDiscoverer", "AutoFixture.Xunit2")]
public sealed class MemberAutoDomainDataAttribute(IFixture fixture, string memberName, params object[] parameters)
    : MemberAutoDataBaseAttribute(fixture, memberName, parameters)
{
    public MemberAutoDomainDataAttribute(string memberName, params object[] parameters)
        : this(new Fixture(), memberName, parameters)
    {
    }

    protected override IAutoFixtureInlineAttributeProvider CreateProvider()
    {
        return new InlineAutoDataAttributeProvider();
    }

    protected override IFixture Customize(IFixture fixture)
    {
        return fixture.NotNull(nameof(fixture))
            .Customize(
                new AutoNSubstituteCustomization
                {
                    ConfigureMembers = true
                });
    }
}
