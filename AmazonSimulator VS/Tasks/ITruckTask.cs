using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Tasks
{
    interface ITruckTask
    {
        void StartTask(Truck t);

        bool TaskComplete(Truck t);
    }
}
