# Fluent Assertion Syntax

In 2025, the FluentAssertions library moved to a paid licensing model, with a rather significant license fee.  While it
was a package we used quite extensively, it was not one that provided functionality over and above that provided by 
other, open-source testing libraries like xUnit - rather it provided more user-friendly syntax that sat on top of those
frameworks.

As a result, we started looking into what it would take to create our own fluent-like syntax library - which has now
become the `Reapit.Platform.Testing.Fluent` namespace!

## Basics

To start an assertion, use the `.Must()` extension.  Each assertion returns an `AndOperator` that allows assertions to 
be chained using the `.And` property.  

```csharp
int i = 1_000;
i.Must().BePositive().And.BeGreaterThan(999);
```

When an assertion is not met, an `XunitException` is thrown which works with the xUnit framework to indicate that the 
containing test has failed.

## Supported Types

- Delegates - `Action`, `Func<T>`, `Func<Task>`, `Func<Task<TResult>>`
- Numeric - `byte`, `byte?`, `sbyte`, `sbyte?`, `short`, `short?`, `ushort`, `ushort?`, `int`, `int?`, `long`, `long?`, 
  `float`, `float?`, `double`, `double?`, `decimal`, `decimal?`
- Struct - `bool`, `bool?`
- Temporal -`DateTime`, `DateTime?`, `DateTimeOffset`, `DateTimeOffset?`
- Collections - `IEnumerable<T>`, `IDictionary<TKey, TResult>`
- Other - `string`, `string?`, `Enum`, `Enum?`, `Guid`, `Guid?`, `HttpResponseMessage`, `ProblemDetails`