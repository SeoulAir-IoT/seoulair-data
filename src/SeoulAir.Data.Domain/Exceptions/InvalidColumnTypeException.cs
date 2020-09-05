using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class InvalidColumnTypeException : Exception
    {
        public InvalidColumnTypeException() { }

        public InvalidColumnTypeException(string message) : base(message) { }

        public InvalidColumnTypeException(string message, Exception innerException) : base(message, innerException) { }

        protected InvalidColumnTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}