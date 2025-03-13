namespace Reapit.Platform.Testing.UnitTests.Fluent.Core;

public class AndOperatorTests
{
    [Fact]
    public void Should_NotExecuteSecondAssertion_WhenFirstFails()
    {
        var predicate = (int? i) =>
        {
            if (i == 1)
                throw new InvalidOperationException();

            return true;
        };

        // If it reaches the second statement, it will throw InvalidOperationException rather than XunitException
        var action = () => 1.Must().BeNegative().And.Match(i => predicate(i));
        action.Must().ThrowExactly<XunitException>();
    }

    [Fact]
    public void Should_ExecuteSecondAssertion_WhenFirstPasses()
    {
        var predicate = (int? i) =>
        {
            if (i == 1)
                throw new InvalidOperationException();

            return true;
        };

        // If it reaches the second statement, it will throw InvalidOperationException rather than XunitException
        var action = () => 1.Must().BePositive().And.Match(i => predicate(i));
        action.Must().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void Should_ExecuteSecondAssertion_WhenSecondFails()
    {
        var action = () => 1.Must().BePositive().And.BeGreaterThan(10);
        action.Must().ThrowExactly<XunitException>();
    }

    [Fact]
    public void Should_Pass_WhenBothStatementsSatisfied()
    {
        var action = () => 1.Must().BePositive().And.BeLessThan(10);
        action.Must().NotThrow();
    }
}