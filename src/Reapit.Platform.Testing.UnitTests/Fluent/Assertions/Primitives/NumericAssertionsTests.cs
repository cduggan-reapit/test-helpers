using Reapit.Platform.Testing.Fluent;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public class NumericAssertionsTests
{
    [Fact]
    public void Should_InitializeForByte() => ((byte)0x01).Must();
    
    [Fact]
    public void Should_InitializeForSignedByte() => ((sbyte)0x01).Must();
    
    [Fact]
    public void Should_InitializeForShort() => ((short)1).Must();
    
    [Fact]
    public void Should_InitializeForUnsignedShort() => ((ushort)1).Must();
    
    [Fact]
    public void Should_InitializeForInt32() => 1.Must();
    
    [Fact]
    public void Should_InitializeForUnsignedInt32() => ((uint)1).Must();
    
    [Fact]
    public void Should_InitializeForLong() => ((long)1).Must();
    
    [Fact]
    public void Should_InitializeForUnsignedLong() => ((ulong)1).Must();

    [Fact] 
    public void Should_InitializeForFloat() => ((float)1).Must();

    [Fact] 
    public void Should_InitializeForDouble() => ((double)1).Must();
    
    [Fact] 
    public void Should_InitializeForDecimal() => ((decimal)1).Must();
    
    public class Be
    {
    }

    public class NotBe
    {
    }

    public class BePositive
    {
    }

    public class BeNegative
    {
    }

    public class BeLessThan
    {
    }

    public class BeLessThanOrEqualTo
    {
    }

    public class BeGreaterThan
    {
    }

    public class BeGreaterThanOrEqualTo
    {
    }

    public class BeInRange
    {
    }

    public class NotBeInRange
    {
    }

    public class Match
    {
    }
}