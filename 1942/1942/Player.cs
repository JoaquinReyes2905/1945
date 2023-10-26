using _1942;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942
{
    internal class Player : Game1
{
    public int vida = 100;
    public int daño = 20;
    public int positionX = 200;
    public int positionY = 350;
    public int score = 0;
    public Texture2D img;
    public Texture2D disparoimg;
    public int municion = 100;
    public bool disparar = true;
    public Player() { }


}
}
