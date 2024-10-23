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
public class InlineAutoDomainDataAttribute(
    IFixture fixture,
    IAutoFixtureInlineAttributeProvider provider,
    params object[] values)
    : InlineAutoDataBaseAttribute(fixture, provider, values)
{
    public InlineAutoDomainDataAttribute(params object[] values)
        : this(new Fixture(), new InlineAutoDataAttributeProvider(), values)
    {
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
