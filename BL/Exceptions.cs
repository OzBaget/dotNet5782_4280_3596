using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BL
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

    [Serializable]
    public class CantSendDroneToCharge : Exception
    {
        public CantSendDroneToCharge(string message) : base(message) { }
    }

    [Serializable]
    public class CantReleaseDroneFromCharge : Exception
    {
        public CantReleaseDroneFromCharge(string message) : base(message) { }
    }

    [Serializable]
    public class CantPickUpParcel : Exception
    {
        public CantPickUpParcel(string message) : base(message) { }
    }

    [Serializable]
    public class CantDeliverParcel : Exception
    {
        public CantDeliverParcel(string message) : base(message) { }
    }

    
}
