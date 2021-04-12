using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class GolfExceptions : Exception
    {
        public GolfExceptions(string message) : base(message)
        {

        }
    }
}
