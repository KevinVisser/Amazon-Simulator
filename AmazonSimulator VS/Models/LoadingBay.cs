using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class LoadingBay : Model3D
    {
        public LoadingBay(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "loadingBay";
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
