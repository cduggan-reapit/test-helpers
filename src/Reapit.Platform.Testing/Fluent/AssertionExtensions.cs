using Reapit.Platform.Testing.Fluent.Assertions.Delegates;
using Reapit.Platform.Testing.Fluent.Assertions.Primitives;

namespace Reapit.Platform.Testing.Fluent;

/// <summary>Extension methods for the creation of assertions objects.</summary>
public static class AssertionExtensions
{
    #region Delegate Assertions

    /// <summary>Create the assertions object for a <see cref="Action"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static ActionAssertions Must(this Action subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="Func{T}"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static FuncAssertions<T> Must<T>(this Func<T> subject) => new(subject);
    
    #endregion
    
    #region Numeric Assertions
    
    /// <summary>Create the assertions object for a <see cref="byte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<byte> Must(this byte subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="sbyte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<sbyte> Must(this sbyte subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="short"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<short> Must(this short subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="ushort"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<ushort> Must(this ushort subject) => new(subject);
    
    /// <summary>Create the assertions object for an <see cref="int"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<int> Must(this int subject) => new(subject);
    
    /// <summary>Create the assertions object for an <see cref="uint"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<uint> Must(this uint subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="long"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<long> Must(this long subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="ulong"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<ulong> Must(this ulong subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="float"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<float> Must(this float subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="double"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<double> Must(this double subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="decimal"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NumericAssertions<decimal> Must(this decimal subject) => new(subject);
    
    #endregion
    
    #region Nullable Numeric Assertions
    
    /// <summary>Create the assertions object for a nullable <see cref="byte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<byte> Must(this byte? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="sbyte"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<sbyte> Must(this sbyte? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="short"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<short> Must(this short? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="ushort"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<ushort> Must(this ushort? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="int"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<int> Must(this int? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="uint"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<uint> Must(this uint? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="long"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<long> Must(this long? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="ulong"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<ulong> Must(this ulong? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="float"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<float> Must(this float? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="double"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<double> Must(this double? subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="decimal"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableNumericAssertions<decimal> Must(this decimal? subject) => new(subject);
    
    #endregion

    #region Temporal Assertions

    /// <summary>Create the assertions object for a <see cref="DateTime"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static DateTimeAssertions Must(this DateTime subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="DateTimeOffset"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static DateTimeOffsetAssertions Must(this DateTimeOffset subject) => new(subject);
    
    /// <summary>Create the assertions object for a nullable <see cref="DateTime"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableDateTimeAssertions Must(this DateTime? subject) => new(subject);
    
    /// <summary>Create the assertions object for a <see cref="DateTimeOffset"/> object.</summary>
    /// <param name="subject">The subject of the assertions.</param>
    public static NullableDateTimeOffsetAssertions Must(this DateTimeOffset? subject) => new(subject);

    #endregion
}