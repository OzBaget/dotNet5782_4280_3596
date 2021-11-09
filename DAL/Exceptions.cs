using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    [Serializable]
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(string message) : base(message) { }
    }

    [Serializable]
    public class IdAlreadyExists : Exception
    {
        public IdAlreadyExists(string message) : base(message) { }
    }
}
