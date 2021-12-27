using System;

namespace DO
{
    [Serializable]
    public class IdNotFoundException : Exception
    {
        public int Id;
        public IdNotFoundException(string message,int id) : base(message) { Id = id; }
    }

    [Serializable]
    public class IdAlreadyExistsException : Exception
    {
        public int Id;
        public IdAlreadyExistsException(string message,int id) : base(message) { Id = id; }
    }
    [Serializable]
    public class NameAlreadyExistsException : Exception
    {
        public string Name;
        public NameAlreadyExistsException(string message, string name) : base(message) { Name = name; }
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
    [Serializable]
    public class LessChargersThanDronesInCharchingException : Exception
    {
        public LessChargersThanDronesInCharchingException(string message) : base(message) { }
    }

}
