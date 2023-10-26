using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942
{
    internal class Municion : Game1
    {
        public int positionX = 0;
        public int positionY = 0;
        public Texture2D imagen;
        int velocidad = 3;

        public Municion(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }
        public Municion() { }

        public Municion(int positionX , int positionY , Texture2D imagen)
        {
            this.positionX=positionX;
            this.positionY=positionY;
            this.imagen = imagen;
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
