using System;
using System.Runtime.Serialization;

namespace SnakeTBs.Identity.LogicLayer.Exceptions.Entities
{
    public class NullTokenException : Exception
    {
        public NullTokenException()
        {
        }

        public NullTokenException(string message) : base(message)
        {
        }

        public NullTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
