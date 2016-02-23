using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.SlowTest
{
    public class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException(string typeName)
            :base("The factory hasn't recognised type: " + typeName)
        {}
    }
}
