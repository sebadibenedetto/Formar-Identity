using Identity.Sdk.Lib.Extensions;
using Identity.Sdk.Lib.Test.Customizations;

namespace Identity.Sdk.Lib.Test.Extensions
{
    public class ObjectExtensionsTests
    {
        public ObjectExtensionsTests()
        {
            EncryptionCustomization.Initialization();
        }

        public class TheMethod_GetKey : ObjectExtensionsTests
        {
            [Fact]
            public void Should_return_Key()
            {
                // Arrange
                var applicationUser = new
                {
                    Name = "Sebastian",
                    Rol = "Admin"
                };

                // Act
                var result = applicationUser.GetKey();

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
