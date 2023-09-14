using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Project8
{
    internal class Player : Game1
    {
        private int vida = 100;
        public int daño = 20;
        public int positionX = 200;
        public int positionY = 350;
        public Texture2D img;
        public Texture2D disparoimg;
        public int PosBalaY = 0;
        public int PosBalaX = 0;
        public bool visible = false;
        int velocidadBala = 10;

        public Player() { }

        public void Disparar()
        {
            if (visible == true)
            {
                if (PosBalaY > 0)
                {
                    PosBalaY -= velocidadBala;
                }
                else
                {
                    PosBalaX = positionX;
                    PosBalaY = positionY;
                    visible = false;
                }
            }
            else
            {
                PosBalaX = positionX;
                PosBalaY = positionY;
            }
        }


    }
}
