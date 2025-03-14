﻿using Reapit.Platform.Common.Extensions;
using Reapit.Platform.Testing.Extensions;
using System.Text;

namespace Reapit.Platform.Testing.UnitTests.Extensions;

public class ObjectExtensionsTests
{
    /*
     * ToStringContent
     */

    [Fact]
    public void ToStringContent_CreatesInstance_WithExpectedContent_ForObject()
    {
        var input = new { Property = "value" };
        var output = input.ToStringContent();
        var expected = new StringContent(input.Serialize(), Encoding.UTF8, "application/json");
        Assert.Equivalent(expected, output);
    }

    [Fact]
    public void ToStringContent_CreatesInstance_WithExpectedContent_ForString()
    {
        const string input = "input";
        var output = input.ToStringContent();
        var expected = new StringContent(input, Encoding.UTF8, "text/plain");
        Assert.Equivalent(expected, output);
    }
}