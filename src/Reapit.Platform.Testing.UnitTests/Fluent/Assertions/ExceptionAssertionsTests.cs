using Reapit.Platform.Testing.Fluent.Assertions;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions;

// Exception is fine here - we're testing things.
#pragma warning disable CA2201
public static class ExceptionAssertionsTests
{
    public class WithMessage
    {
        [Fact]
        public void Should_Throw_WhenNoComparerProvided_AndStringsDoNotMatch()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("different");
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotThrow_WhenNoComparerProvided_AndStringsMatch_CaseInsensitive()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("EXAMPLE");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotThrow_WhenNoComparerProvided_AndStringsIdentical()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("example");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Throw_WhenCaseInsensitiveComparerProvided_AndStringsDoNotMatch()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("different", StringComparison.OrdinalIgnoreCase);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotThrow_WhenCaseInsensitiveComparerProvided_AndStringsMatch_CaseInsensitive()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("EXAMPLE", StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotThrow_WhenCaseInsensitiveComparerProvided_AndStringsIdentical()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("example", StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Throw_WhenCaseSensitiveComparerProvided_AndStringsDoNotMatch()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("different", StringComparison.Ordinal);
            action.Must().Throw();
        }

        [Fact]
        public void Should_Throw_WhenCaseSensitiveComparerProvided_AndStringsMatch_CaseInsensitive()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("EXAMPLE", StringComparison.Ordinal);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotThrow_WhenCaseSensitiveComparerProvided_AndStringsIdentical()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithMessage("example", StringComparison.Ordinal);
            action.Must().NotThrow();
        }
    }

    public class WithInnerException
    {
        [Fact]
        public void Should_Throw_WhenNoInnerException()
        {
            var exception = new Exception("example");
            var action = () => exception.CreateSut().WithInnerException<Exception>();
            action.Must().Throw();
        }

        [Fact]
        public void Should_Throw_WhenInnerExceptionOfDifferentType()
        {
            var exception = new Exception("example", new InvalidOperationException());
            var action = () => exception.CreateSut().WithInnerException<ArgumentException>();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotThrow_WhenInnerExceptionOfDerivedType()
        {
            var exception = new Exception("example", new ArgumentException());
            var action = () => exception.CreateSut().WithInnerException<Exception>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotThrow_WhenInnerExceptionOfType()
        {
            var exception = new Exception("example", new ArgumentException());
            var action = () => exception.CreateSut().WithInnerException<ArgumentException>();
            action.Must().NotThrow();
        }
    }

    /*
     * Private methods
     */

    private static ExceptionAssertions<TException> CreateSut<TException>(this TException exception)
        where TException : Exception => new(exception);
}
#pragma warning restore CA2201