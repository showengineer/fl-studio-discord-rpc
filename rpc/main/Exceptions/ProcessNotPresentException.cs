using System;

namespace FLRPC.Exceptions
{
    class ProcessNotPresentException : Exception
    {
        public ProcessNotPresentException(string message) : base(message) { }
    }
}
