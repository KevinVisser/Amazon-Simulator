using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Truck : Model3D
    {
        public Truck(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "truck";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }
    }
}
