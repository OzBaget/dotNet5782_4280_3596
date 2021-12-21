using System.Collections.Generic;
using System;

namespace Dal
{
    sealed partial class DalObject: DalApi.IDal
    {
        static readonly DalApi.IDal instance = new DalObject();
        public static DalApi.IDal Instance { get => instance; }
        public DalObject()
        {
            DataSource.Initialize();
        }
    }
}
