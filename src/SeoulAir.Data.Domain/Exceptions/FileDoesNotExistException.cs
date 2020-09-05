using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class FileDoesNotExistException : Exception
    {
        public FileDoesNotExistException() { }

        public FileDoesNotExistException(string message) : base(message) { }

        public FileDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }

        protected FileDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
