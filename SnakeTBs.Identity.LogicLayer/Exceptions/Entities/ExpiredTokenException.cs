using System;
using System.Runtime.Serialization;

namespace SnakeTBs.Identity.LogicLayer.Exceptions.Entities
{
    public class ExpiredTokenException : Exception
    {
        public ExpiredTokenException()
        {
        }

        public ExpiredTokenException(string message) : base(message)
        {
        }

        public ExpiredTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExpiredTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
