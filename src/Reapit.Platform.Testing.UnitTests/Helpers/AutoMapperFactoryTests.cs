using AutoMapper;
using Reapit.Platform.Testing.Helpers;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class AutoMapperFactoryTests
{
    public class Create
    {
        [Fact]
        public void Should_CreateAutoMapperInstance_ForProfile()
        {
            var input = new TestProfile.TestInputModel("123");
            var expected = new TestProfile.TestOutputModel(123);

            var sut = AutoMapperFactory.Create<TestProfile>();
            var actual = sut.Map<TestProfile.TestOutputModel>(input);
            Assert.Equivalent(expected, actual);
        }
    }
}

internal sealed class TestProfile : Profile
{
    public sealed record TestInputModel(string Input);

    public sealed record TestOutputModel(int? Output);

    // You can't use out parameters in configured mapping, so we use this method instead
    private static int? MapInput(string input) => int.TryParse(input, out var cast) ? cast : null;

    public TestProfile()
    {
        CreateMap<TestInputModel, TestOutputModel>()
            .ForCtorParam(nameof(TestOutputModel.Output), ops => ops.MapFrom(src => MapInput(src.Input)));
    }
}