using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data
{
    public class DatabaseAccessException : Exception
    {
        public DatabaseAccessException(string message, Exception e = null) : base(message, e)
        {

        }
    }
}
