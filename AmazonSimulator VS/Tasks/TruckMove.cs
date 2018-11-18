using Models;
using PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks
{
    public class TruckMove : ITruckTask
    {
        private List<Node> path;

        public TruckMove(List<Node> path)
        {
            this.path = path;
        }

        public void StartTask(Truck t)
        {
            t.MoveOverPath(this.path);
        }

        public bool TaskComplete(Truck t)
        {
            return t.x == path.Last().GetX() && t.z == path.Last().GetZ();
        }
    }
}
