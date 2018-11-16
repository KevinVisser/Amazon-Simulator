using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PathFinding;
using Tasks;

namespace Models
{
    public class Robot : Model3D
    {
        private List<IRobotTask> tasks = new List<IRobotTask>();

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "robot";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }

        public override bool Update(int tick)
        {
            if(tasks != null)
            {
                if (tasks.First().TaskComplete(this))
                {
                    tasks.RemoveAt(0);

                    if (tasks.Count == 0)
                    {
                        tasks = null;
                    }
                    else
                    {
                        tasks.First().StartTask(this);
                    }

                }
                else
                {
                    tasks.First().StartTask(this);
                }
            }
            return base.Update(tick);
        }

        public void AddTask(IRobotTask task)
        {
            tasks.Add(task);
        }

        public void MoveOverPath(List<Node> path)
        {
            if(path.Count > 1)
            {
                this.Move(path[0]._x, path[0]._y, path[0]._z);
                path.RemoveAt(0);
            }
            else
            {
                this.Move(path[0]._x, path[0]._y, path[0]._z);
            }
        }
    }
}