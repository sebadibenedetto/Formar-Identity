using System.Runtime.Serialization;

namespace Identity.Sdk.Lib.Exception
{
    [Serializable()]

    public class InvalidUserCredentialsException : System.Exception
    {
        public InvalidUserCredentialsException()
             : base(Domain.Globalization.Message.InvalidUserCredentials) { }
        public InvalidUserCredentialsException(string message) : base(message) { }

        public InvalidUserCredentialsException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidUserCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
