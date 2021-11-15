using System.Collections.Generic;
using IDAL.DO;
using IDAL;
using System;
namespace DalObject
{
    public partial class DalObject: IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        
    }
}
