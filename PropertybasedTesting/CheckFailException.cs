using System;
using System.Runtime.Serialization;

namespace PropertybasedTesting
{
    public class CheckFailException : Exception
    {
        public CheckFailException()
        { }

        public CheckFailException(string message)
            : base(message)
        { }

        public CheckFailException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected CheckFailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}