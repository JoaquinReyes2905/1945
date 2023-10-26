using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942
{
    internal class Enemigo : Game1
    {
        public int vida = 20;
        public int daño = 10;
        public int positionX = 0;
        public int positionY = 0;
        public Texture2D img;
        public int velocidad = 2;
        public Enemigo() { }

        public Enemigo(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }
        public Enemigo(int positionX, int positionY, int velocidad)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.velocidad = velocidad;
        }
        public Enemigo(int positionX, int positionY, int velocidad, Texture2D img)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.velocidad = velocidad;
            this.img = img;
        }

        public void mover(Texture2D enemy)
        {
            if (positionY <= Window.ClientBounds.Height + enemy.Height)
            {
                positionY += velocidad;
            }
        }
    }
}
