using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportManager
{
    interface ISport
    {
        void UpdateIt(SportObject sn, AllData data);
        SportObject RemoveIt(AllData data);
    }
}
