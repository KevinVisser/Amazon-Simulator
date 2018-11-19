using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Laaddok class
    /// </summary>
    public class LoadingBay : Model3D
    {
        /// <summary>
        /// constructor voor het aanmaken van een laaddok
        /// </summary>
        /// <param name="x">Positie op de x as</param>
        /// <param name="y">Positie op de y as</param>
        /// <param name="z">Positie op de z as</param>
        /// <param name="rotationX">Rotatie op de x as</param>
        /// <param name="rotationY">Rotatie op de y as</param>
        /// <param name="rotationZ">Rotatie op de z as</param>
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
