using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class NegativeNumberIsNotAllowedException : Exception
    {
        public NegativeNumberIsNotAllowedException()
        {
        }

        public NegativeNumberIsNotAllowedException(string message) : base(message)
        {
        }

        public NegativeNumberIsNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
