using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942
{
    internal class Obstaculo : Game1
    {
        public int positionX = 0;
        public int positionY = 0;
        int velocidad = 4;

        public Obstaculo(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public void mover(Texture2D imagen)
        {
            if (positionY <= Window.ClientBounds.Height + imagen.Height)
            {
                positionY += velocidad;
            }
        }
    }
}
