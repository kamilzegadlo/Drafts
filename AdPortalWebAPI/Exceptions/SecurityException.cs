using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdPortalWebAPI.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        {
            
        }
    }
}