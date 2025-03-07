using Reapit.Platform.Testing.Fluent.Assertions.Delegates;
using Reapit.Platform.Testing.Fluent.Assertions.Primitives;

namespace Reapit.Platform.Testing.Fluent;

/// <summary>Extension methods for the creation of assertions objects.</summary>
public static class AssertionExtensions
{
    #region Delegate Assertions

    /// <summary>Create the assertions object for an <see langword="Action"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static ActionAssertions Must(this Action subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="Func{T}"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static FuncAssertions<T> Must<T>(this Func<T> subject) => new(subject);
    
    #endregion
    
    #region Numeric Assertions
    
    /// <summary>Create the assertions object for a <see langword="byte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<byte> Must(this byte subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="sbyte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<sbyte> Must(this sbyte subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="short"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<short> Must(this short subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="ushort"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<ushort> Must(this ushort subject) => new(subject);
    
    /// <summary>Create the assertions object for an <see langword="int"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<int> Must(this int subject) => new(subject);
    
    /// <summary>Create the assertions object for an <see langword="uint"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<uint> Must(this uint subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="long"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<long> Must(this long subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="ulong"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<ulong> Must(this ulong subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="float"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<float> Must(this float subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="double"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<double> Must(this double subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="decimal"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<decimal> Must(this decimal subject) => new(subject);
    
    #endregion
    
    #region Nullable Numeric Assertions
    
    /// <summary>Create the assertions object for a nullable <see langword="byte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<byte> Must(this byte? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="sbyte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<sbyte> Must(this sbyte? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="short"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<short> Must(this short? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="ushort"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<ushort> Must(this ushort? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="int"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<int> Must(this int? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="uint"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<uint> Must(this uint? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="long"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<long> Must(this long? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="ulong"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<ulong> Must(this ulong? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="float"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<float> Must(this float? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="double"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<double> Must(this double? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="decimal"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<decimal> Must(this decimal? subject) => new(subject);
    
    #endregion

    #region Temporal Assertions

    /// <summary>Create the assertions object for a <see langword="DateTime"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static DateTimeAssertions Must(this DateTime subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="DateTimeOffset"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static DateTimeOffsetAssertions Must(this DateTimeOffset subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see langword="DateTime"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableDateTimeAssertions Must(this DateTime? subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see langword="DateTimeOffset"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableDateTimeOffsetAssertions Must(this DateTimeOffset? subject) => new(subject);

    #endregion

    /// <summary>Create the assertions object for a <see langword="string"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static StringAssertions Must(this string subject) => new(subject);
}