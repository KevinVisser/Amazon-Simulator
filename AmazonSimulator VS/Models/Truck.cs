using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Truck : Model3D
    {
        private PalletRack rack = null;

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

        public override bool Update(int tick)
        {
            if(this.rack != null)
            {
                this.rack.Move(this.x - 1.3, this.y + 1, this.z);
                this.Move(this._x + 0.1, this._y, this._z);
            }
            return base.Update(tick);
        }

        public void GetRack(PalletRack p)
        {
            this.rack = p;
        }
    }

}
