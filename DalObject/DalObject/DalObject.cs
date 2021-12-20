using System.Collections.Generic;
using System;
using DalApi;

namespace Dal
{
    public partial class DalObject: IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
    }
}
