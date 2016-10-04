using System;
using System.Runtime.Serialization;

namespace Tanks
{
    class GameException : ApplicationException
    {
        public GameException()
        {

        }
        public GameException(string str) : base(str)
        {

        }

        public GameException(string str, Exception ex) : base (str, ex)
        {

        }
        protected GameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
