using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Negocio.Util
{
    [Serializable]
    public class ClublyStateException : System.Exception
    {
        public ClublyStateException()
        {
        }

        public ClublyStateException(string message)
            : base(message)
        {
        }

        public ClublyStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ClublyStateException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public ClublyStateException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected ClublyStateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}