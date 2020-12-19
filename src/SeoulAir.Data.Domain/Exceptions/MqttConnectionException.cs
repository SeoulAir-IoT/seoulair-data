using System;
using System.Runtime.Serialization;

namespace SeoulAir.Data.Domain.Exceptions
{
    [Serializable]
    public class MqttConnectionException : Exception
    {
        public MqttConnectionException() { }

        public MqttConnectionException(string message) : base(message) { }

        public MqttConnectionException(string message, Exception innerException) : base(message, innerException) { }

        protected MqttConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
