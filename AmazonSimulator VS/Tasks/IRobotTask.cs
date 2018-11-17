﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Tasks
{
    public interface IRobotTask
    {
        void StartTask(Robot r);

        bool TaskComplete(Robot r);
    }
}
