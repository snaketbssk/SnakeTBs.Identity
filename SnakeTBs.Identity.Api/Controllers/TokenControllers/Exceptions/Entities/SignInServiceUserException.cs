using System;
using System.Runtime.Serialization;

namespace SnakeTBs.Identity.Api.Controllers.TokenControllers.Exceptions.Entities
{
    public class SignInServiceUserException : Exception
    {
        public SignInServiceUserException()
        {
        }

        public SignInServiceUserException(string message) : base(message)
        {
        }

        public SignInServiceUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SignInServiceUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
