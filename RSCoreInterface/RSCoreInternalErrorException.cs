using System;

namespace RSCoreInterface
{
    public class RSCoreInternalErrorException : ApplicationException
    {
        internal RSCoreInternalErrorException()
        {
            return;
        }

        internal RSCoreInternalErrorException(string message)
            : base (message)
        {
            return;
        }

        internal RSCoreInternalErrorException(string message, params object[] args)
            : base(SafeFormatMessage(message, args))
        {
            return;
        }

        internal RSCoreInternalErrorException(Exception innerException, string message)
            : base(message, innerException)
        {
            return;
        }

        internal RSCoreInternalErrorException(Exception innerException, string message, params object[] args)
            : base(SafeFormatMessage(message, args), innerException)
        {
            return;
        }

        private static string SafeFormatMessage(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message)) { return "<NULL>"; }
            try { return string.Format(message, args); }
            catch { return message; }
        }
    }
}
