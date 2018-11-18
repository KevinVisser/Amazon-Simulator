using PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks;

namespace Models
{
    public class Truck : Model3D
    {
        private List<ITruckTask> tasks = new List<ITruckTask>();
        private PalletRack rack = null;

        private List<Node> currentPath = new List<Node>();
        private List<Node> path = new List<Node>();
        private string currentNode;
        private string _name;

        public Truck(double x, double y, double z, double rotationX, double rotationY, double rotationZ, string name)
        {
            type = "truck";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
            _name = name;
        }

        internal void MoveOverPath(List<Node> path)
        {
            this.currentPath = path;
        }

        public override bool Update(int tick)
        {
            if (tasks != null)
            {
                if (tasks.First().TaskComplete(this))
                {
                    tasks.RemoveAt(0);
                    if (currentPath != null && isEnd(this))
                    {
                        this.rack.Move(this.x + 1000, this.y, this.z);
                        this.rack = null;

                        this.Move(this.x, this.y, this.z + 90);
                        currentPath = Pathfinding.Listnodes(currentNode, "Y" + this._name, Pathfinding.listOfNodes, this.path);
                        this.AddTask(new TruckMove(currentPath));
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
            if (this.rack != null)
            {
                this.rack.Move(this.x, this.y + 1, this.z + 1.5);
                if(tasks == null)
                {
                    currentPath.Clear();
                    this.currentPath = Pathfinding.Listnodes(currentNode, "Z" + this._name, Pathfinding.listOfNodes, this.path);
                    tasks = new List<ITruckTask>();
                    this.AddTask(new TruckMove(this.currentPath));
                }
            }
            if (this.currentPath != null && this.currentPath.Count != 0)
            {
                this._x = Math.Round(this._x, 1);
                this._z = Math.Round(this._z, 1);
                
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
                    this.currentNode = currentPath[0].GetName();
                    currentPath.RemoveAt(0);
                }
            }
            return base.Update(tick);
        }

        public bool isHome(Truck t)
        {
            if ((t.x == 33 || t.x == 35 || t.x == 37) && t.z == 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetRack(PalletRack p)
        {
            this.rack = p;
        }

        public void AddTask(ITruckTask task)
        {
            tasks.Add(task);
        }

        public bool isEnd(Truck t)
        {
            if ((t.x == 33 || t.x == 35 || t.x == 37) && t.z == -30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
