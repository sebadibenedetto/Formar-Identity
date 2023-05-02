using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Identity.Sdk.Lib.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetKey(this object obj)
        {
            var objEncrypted = Encryption.Encrypt(JsonConvert.SerializeObject(obj));

            return WebEncoders.Base64UrlEncode(objEncrypted);
        }
    }
}
