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
    public class IdAlreadyExistsException : Exception
    {
        public IdAlreadyExistsException(string message) : base(message) { }
    }

    [Serializable]
    public class CantSendDroneToChargeException : Exception
    {
        public CantSendDroneToChargeException(string message) : base(message) { }
    }

    [Serializable]
    public class CantReleaseDroneFromChargeException : Exception
    {
        public CantReleaseDroneFromChargeException(string message) : base(message) { }
    }

    [Serializable]
    public class CantPickUpParcelException : Exception
    {
        public CantPickUpParcelException(string message) : base(message) { }
    }

    [Serializable]
    public class CantDeliverParcelException : Exception
    {
        public CantDeliverParcelException(string message) : base(message) { }
    }

}
