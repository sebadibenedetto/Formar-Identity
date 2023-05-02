using Identity.Sdk.Lib.Extensions;

namespace Identity.Sdk.Lib.Test.Customizations
{
    internal static class EncryptionCustomization
    {
        private const string Key = "GfCBTd5s6v8yBEHMbQeThWmZq4t7";
        private const string IV = "IS$4sAjHQ$2pIj12";
        private const string Salt = "FastFood";

        public static void Initialization()
        {
            Encryption.Initialization(Key, IV, Salt);
        }
    }
}
