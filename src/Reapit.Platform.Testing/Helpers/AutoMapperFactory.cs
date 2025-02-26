using AutoMapper;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Factory for AutoMapper profile instances.</summary>
public static class AutoMapperFactory
{
    /// <summary>Create an instance of <typeparamref name="TProfile"/>.</summary>
    /// <typeparam name="TProfile">The type of the AutoMapper profile to instantiate.</typeparam>
    /// <remarks>
    /// <para>
    /// This is just shorthand because we repeat the same, lengthy definition in a lot of test classes.
    /// </para>
    /// <para>Instead of:
    /// <code>
    /// private readonly IMapper _mapper
    ///     = new MapperConfiguration(cfg
    ///             => cfg.AddProfile&lt;MyProfile&gt;())
    ///         .CreateMapper(); 
    /// </code>
    /// </para>
    /// <para>
    /// We can shorten the declaration to:
    /// <code>
    /// private readonly IMapper _mapper
    ///     = AutoMapperFactory.Create&lt;MyProfile&gt;()
    /// </code>
    /// </para>
    /// </remarks>
    public static IMapper Create<TProfile>() where TProfile : Profile
        => new MapperConfiguration(cfg => cfg.AddProfile(typeof(TProfile))).CreateMapper();
}