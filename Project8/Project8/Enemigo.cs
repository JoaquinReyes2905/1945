using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project8
{
    internal class Enemigo : Game1
    {
        public int vida = 20;
        public int daño = 10;
        public int positionX = 0;
        public int positionY= 0;
        public Texture2D img;
        public Enemigo() { }
    }
}
