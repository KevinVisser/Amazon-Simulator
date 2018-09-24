using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Node
    {
        private string _nodeName = "";
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;

        public Node(string nodeName, double x, double y, double z)
        {
            _nodeName = nodeName;
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
