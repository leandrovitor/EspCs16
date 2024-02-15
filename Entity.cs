using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspCs16
{
    class Entity
    {
        public int hp;
        public Vector3 xyz;

        public Point top, bottom;
        public Rectangle rect()
        {
            return new Rectangle
            {
                Location = new Point(Bottom.x - (bottom.x - top.X), bottom.Y - (bottom.Y - top.Y) / 2),
                size = new Size((bottom.Y - top.Y) / 2, (bottom.Y - top.Y))

            };
        }


        public class vector3
        {
            public float x, y, z;
        }

        public class ViewMatrix
        {
            public float m11, m12, m13, m14;
            public float m21, m22, m23, m24;
            public float m31, m32, m33, m34;
            public float m41, m42, m43, m44;
        }
    }
}