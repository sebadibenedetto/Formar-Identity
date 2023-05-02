namespace Identity.Sdk.Lib.Extensions
{
    public static class Argument
    {
        public static void ThrowIfNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}