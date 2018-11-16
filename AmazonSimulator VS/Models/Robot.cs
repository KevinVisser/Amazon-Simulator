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
        private List<Node> currentPath = null;
        private PalletRack rack = null;

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
            }

            if(this.currentPath != null && this.currentPath.Count != 0)
            {
                this._x = Math.Round(this._x, 1);
                this._z = Math.Round(this._z, 1);
                //bewegen
                if (currentPath[0].HasRack() && rack == null)
                {
                    if(this._x == currentPath[0]._x && this._z == currentPath[0]._z)
                    {
                        this.rack = currentPath[0].GetRack();
                        this.rack.Move(this.x, this.y + 1.4, this.z);
                    }
                    else
                    {
                        if (this._x < currentPath[0]._x)
                        {
                            this.Move(this.x + 0.1, this.y, this.z);
                        }
                        else if (this._x > currentPath[0]._x)
                        {
                            this.Move(this.x - 0.1, this.y, this.z);
                        }
                        else if (this._z < currentPath[0]._z)
                        {
                            this.Move(this.x, this.y, this.z + 0.1);
                        }
                        else if (this._z > currentPath[0]._z)
                        {
                            this.Move(this.x, this.y, this.z - 0.1);
                        }
                        else
                        {
                            currentPath.RemoveAt(0);
                        }
                    }
                }
                else if(rack != null)
                {
                    if (this._x < currentPath[0]._x)
                    {
                        this.Move(this.x + 0.1, this.y, this.z);
                        this.rack.Move(this.x + 0.1, this.y + 1.4, this.z);
                    }
                    else if (this._x > currentPath[0]._x)
                    {
                        this.Move(this.x - 0.1, this.y, this.z);
                        this.rack.Move(this.x - 0.1, this.y + 1.4, this.z);
                    }
                    else if (this._z < currentPath[0]._z)
                    {
                        this.Move(this.x, this.y, this.z + 0.1);
                        this.rack.Move(this.x, this.y + 1.4, this.z + 0.1);
                    }
                    else if (this._z > currentPath[0]._z)
                    {
                        this.Move(this.x, this.y, this.z - 0.1);
                        this.rack.Move(this.x, this.y + 1.4, this.z - 0.1);
                    }
                    else
                    {
                        currentPath.RemoveAt(0);
                    }
                }
                else
                {
                    if (this._x < currentPath[0]._x)
                    {
                        this.Move(this.x + 0.1, this.y, this.z);
                    }
                    else if (this._x > currentPath[0]._x)
                    {
                        this.Move(this.x - 0.1, this.y, this.z);
                    }
                    else if (this._z < currentPath[0]._z)
                    {
                        this.Move(this.x, this.y, this.z + 0.1);
                    }
                    else if (this._z > currentPath[0]._z)
                    {
                        this.Move(this.x, this.y, this.z - 0.1);
                    }
                    else
                    {
                        currentPath.RemoveAt(0);
                    }
                }
                //kijken of je robot op de node zit kijk of die locate een rack heeft (hasRack) en zet de rack in de robot. obj
            }

            return base.Update(tick);
        }

        public void AddTask(IRobotTask task)
        {
            tasks.Add(task);
        }

        public void MoveOverPath(List<Node> path)
        {
            this.currentPath = path;
        }
    }
}