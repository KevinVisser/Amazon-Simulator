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
        private List<Node> path = new List<Node>();
        private PalletRack rack = null;
        private Truck _truck;

        private char _name;
        private string currentNode;
        int i = 1;

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ, char name, Truck truck)
        {
            type = "robot";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;

            _name = name;
            _truck = truck;
        }

        public override bool Update(int tick)
        {
            if (tasks != null)
            {
                if (tasks.First().TaskComplete(this))
                {
                    tasks.RemoveAt(0);
                    if(currentPath != null && isHome(this) && this._truck.isHome(this._truck))
                    {
                        this.GiveRack();
                        if(i < 4)
                        {
                            this.currentPath.Clear();
                            this.currentPath = Pathfinding.Listnodes("A", NextRack(this, i), Pathfinding.listOfNodes, this.path);
                            this.AddTask(new RobotMove(this.currentPath));
                            i++;
                        }
                    }

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

            if(tasks == null && currentPath != null && isHome(this) && this._truck.isHome(this._truck) && this.rack != null)
            {
                tasks = new List<IRobotTask>();
                this.GiveRack();
                if (i < 4)
                {
                    this.currentPath.Clear();
                    this.currentPath = Pathfinding.Listnodes("A", NextRack(this, i), Pathfinding.listOfNodes, this.path);
                    this.AddTask(new RobotMove(this.currentPath));
                    i++;
                }
                else
                {
                    tasks = null;
                }
            }

            if (this.currentPath != null && this.currentPath.Count != 0)
            {
                this._x = Math.Round(this._x, 1);
                this._z = Math.Round(this._z, 1);
                //bewegen
                if (currentPath[0].HasRack() && rack == null && currentPath.Count == 1)
                {
                    if(this._x == currentPath[0].GetX() && this._z == currentPath[0].GetZ())
                    {
                        this.rack = currentPath[0].GetRack();
                        this.rack.Move(this.x, this.y + 1.4, this.z);

                        this.currentNode = currentPath[0].GetName();
                        currentPath[0].SetRack(null);
                        currentPath.Clear();

                        this.currentPath = Pathfinding.Listnodes(currentNode, "J", Pathfinding.listOfNodes, this.path);
                        if(tasks == null)
                        {
                            tasks = new List<IRobotTask>();
                            this.AddTask(new RobotMove(this.currentPath));
                        }
                    }
                }
                if(rack != null)
                {
                    this.rack.Move(this.x, this.y + 1.4, this.z);
                }

                if (this._x < currentPath[0].GetX())
                {
                    this.Move(this.x + 0.1, this.y, this.z);
                }
                else if (this._x > currentPath[0].GetX())
                {
                    this.Move(this.x - 0.1, this.y, this.z);
                }
                else if (this._z < currentPath[0].GetZ())
                {
                    this.Move(this.x, this.y, this.z + 0.1);
                }
                else if (this._z > currentPath[0].GetZ())
                {
                    this.Move(this.x, this.y, this.z - 0.1);
                }
                else
                {
                    currentPath.RemoveAt(0);
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
            this.currentPath = path;
        }

        private static bool isHome(Robot r)
        {
            if(r.x == 28 && r.z == 17)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string NextRack(Robot r, int num)
        {
            string next = "";
            int a = 4 - num;
            next = r._name + Convert.ToString(a);
            return next;
        }

        private void GiveRack()
        {
            this._truck.GetRack(this.rack);
            this.rack = null;
        }
    }
}