using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Reflection;

namespace Dal
{
    sealed partial class DalObject: DalApi.IDal
    {
        static readonly DalApi.IDal instance = new DalObject();
        public static DalApi.IDal Instance { get => instance; }
        DalObject()
        {
            DataSource.Initialize();
        }
        
    }
}
