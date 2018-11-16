using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Node
    {
        public string _nodeName = "";
        public double _x = 0;
        public double _y = 0;
        public double _z = 0;
        private PalletRack rack = null;
        public List<string> Neighbors = new List<string>();

        public Node(string nodeName, double x, double y, double z, string neighbor1, string neighbor2 = null)
        {
            _nodeName = nodeName;
            _x = x;
            _y = y;
            _z = z;

            Neighbors.Add(neighbor1);
            if (neighbor2 != null)
            {
                Neighbors.Add(neighbor2);
            }
        }

        public void SetRack(PalletRack p)
        {
            this.rack = p;
        }

        public bool HasRack()
        {
            if(this.rack != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PalletRack GetRack()
        {
            return rack;
        }
    }
}
