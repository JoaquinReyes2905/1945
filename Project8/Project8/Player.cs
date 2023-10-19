using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
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
        public int vida = 100;
        public int daño = 20;
        public int positionX = 200;
        public int positionY = 350;
        public int score = 0;
        public Texture2D img;
        public Texture2D disparoimg;

        public Player() { }


    }
}
