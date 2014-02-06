using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET
{
    public class UserAlreadyLoggedInException : Exception
    {
        public UserAlreadyLoggedInException(string message)
            : this(message, null)
        {
        }

        public UserAlreadyLoggedInException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class NotLoggedInException : Exception
    {
        public NotLoggedInException(string message)
            : this(message, null)
        {
        }

        public NotLoggedInException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message)
            : this(message, null)
        {
        }

        public InvalidCredentialsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}