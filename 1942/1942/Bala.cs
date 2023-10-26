using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942
{
    internal class Bala: Game1
    {
        public int PosBalaY = 0;
        public int PosBalaX = 0;
        public bool visible = false;
        int velocidadBala = 5;

        public Bala() { }
        public void Disparar(Player player)
        {
            if (visible == true)
            {
                if (PosBalaY > 0)
                {
                    PosBalaY -= velocidadBala;
                }
                else
                {
                    PosBalaX = player.positionX;
                    PosBalaY = player.positionY;
                    visible = false;
                }
            }
            else
            {
                PosBalaX = player.positionX;
                PosBalaY = player.positionY;
            }
        }
    }
}
