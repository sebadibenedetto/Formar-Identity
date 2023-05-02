using Identity.Sdk.Lib.Test.Customizations;

namespace Identity.Sdk.Lib.Test.Extensions
{
    public class EncryptionTest
    {
        public EncryptionTest() 
        {
            EncryptionCustomization.Initialization();
        }

        public class TheMethod_Encrypt : EncryptionTest 
        {
            [Fact]
            public void Should_return_encrypted_when_send_string()
            {
                // Arrange

                // Act

                // Assert
            }
        }

        public class TheMethod_Decrypt : EncryptionTest 
        {

        }
    }
}
