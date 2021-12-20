using System.Collections.Generic;
using System;

namespace Dal
{
    public partial class DalObject: DalApi.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
    }
}
