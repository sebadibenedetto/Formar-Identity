using FluentAssertions;
using Identity.Sdk.Lib.Extensions;

namespace Identity.Sdk.Lib.Test.Extensions
{
    public class ArgumentTest
    {
        public class TheMethod_ThrowIfNull : ArgumentTest
        {
            [Theory]
            [InlineData("UserSessionService")]
            [InlineData("DbContex")]
            public void Should_throw_ArgumentNullException_when_object_is_null(string parameterName)
            {
                // Act
                var result = Record.Exception(() => Argument.ThrowIfNull(null, parameterName));

                // Assert
                result.Should().BeOfType<ArgumentNullException>();
                result?.Message.Should().Be($"Value cannot be null. (Parameter '{parameterName}')");
            }
        }
    }
}
