using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class InvalidStationCodeFormatException : Exception
    {
        public InvalidStationCodeFormatException() { }

        public InvalidStationCodeFormatException(string message) : base(message) { }

        public InvalidStationCodeFormatException(string message, Exception innerException) 
            : base(message, innerException) { }

        protected InvalidStationCodeFormatException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
