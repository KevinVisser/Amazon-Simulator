using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// class voor alle modellen met hun variabelen
    /// </summary>
    public class Model3D : IUpdatable
    {
        /// <summary>
        /// x positie
        /// </summary>
        protected double _x = 0;
        /// <summary>
        /// y positie
        /// </summary>
        protected double _y = 0;
        /// <summary>
        /// z positie
        /// </summary>
        protected double _z = 0;
        /// <summary>
        /// x rotatie
        /// </summary>
        protected double _rX = 0;
        /// <summary>
        /// y rotatie
        /// </summary>
        protected double _rY = 0;
        /// <summary>
        /// z rotatie
        /// </summary>
        protected double _rZ = 0;

        /// <summary>
        /// het type object (truck, robot, pallet etc.)
        /// </summary>
        public string type;
        /// <summary>
        /// een uniek ID per object
        /// </summary>
        public Guid guid;
        /// <summary>
        /// krijg x positie van object
        /// </summary>
        public double x { get { return _x; } }
        /// <summary>
        /// krijg y positie van object
        /// </summary>
        public double y { get { return _y; } }
        /// <summary>
        /// krijg z positie van object
        /// </summary>
        public double z { get { return _z; } }
        /// <summary>
        /// krijg x rotatie van object
        /// </summary>
        public double rotationX { get { return _rX; } }
        /// <summary>
        /// krijg y rotatie van object
        /// </summary>
        public double rotationY { get { return _rY; } }
        /// <summary>
        /// krijg z rotatie van object
        /// </summary>
        public double rotationZ { get { return _rZ; } }

        /// <summary>
        /// of het object moet updaten
        /// </summary>
        public bool needsUpdate = true;


        /// <summary>
        /// Het bewegen van een object
        /// </summary>
        /// <param name="x">x positie</param>
        /// <param name="y">y positie</param>
        /// <param name="z">z positie</param>
        public virtual void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = true;
        }

        /// <summary>
        /// Roteren van het object
        /// </summary>
        /// <param name="rotationX">x rotatie</param>
        /// <param name="rotationY">x rotatie</param>
        /// <param name="rotationZ">x rotatie</param>
        public virtual void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }

        /// <summary>
        /// Het updaten van het object (positie etc.)
        /// </summary>
        /// <param name="tick">Hoe vaak per seconde</param>
        /// <returns></returns>
        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}