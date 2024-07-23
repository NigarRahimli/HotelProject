using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Exceptions
{
    public class OwnerAccessException : Exception
    {
        public OwnerAccessException(string message) : base(message)
        {
        }
    }
}
