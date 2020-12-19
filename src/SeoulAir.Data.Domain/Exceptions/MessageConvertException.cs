using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class MessageConvertException : Exception
    {
        public MessageConvertException() { }

        public MessageConvertException(string message) : base(message) { }

        public MessageConvertException(string message, Exception innerException) : base(message, innerException) { }

        protected MessageConvertException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
