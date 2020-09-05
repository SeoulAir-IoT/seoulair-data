using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class InvalidDateTimeFormatException : Exception
    {
        public InvalidDateTimeFormatException() { }

        public InvalidDateTimeFormatException(string message) : base(message) { }

        public InvalidDateTimeFormatException(string message, Exception innerException) : base(message, innerException) { }

        protected InvalidDateTimeFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}