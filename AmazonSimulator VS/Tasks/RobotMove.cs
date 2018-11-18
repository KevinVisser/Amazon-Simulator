using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using PathFinding;

namespace Tasks
{
    public class RobotMove : IRobotTask
    {
        private bool startupComplete = false;
        private bool complete = false;

        private List<Node> path;

        public RobotMove(List<Node> path)
        {
            this.path = path;
        }

        public void StartTask(Robot r)
        {
            r.MoveOverPath(this.path);
        }

        public bool TaskComplete(Robot r)
        {
            return r.x == path.Last().GetX() && r.z == path.Last().GetZ();
        }
    }
}
