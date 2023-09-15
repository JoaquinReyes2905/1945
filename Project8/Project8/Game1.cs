using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms.VisualStyles;

namespace Project8
{
    public class Game1 : Game
    {
        List<Enemigo> enemigos;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D backgroundTexture;
        Player player;
        DateTime timew;
        DateTime tiempoActual;
        TimeSpan tiempo;
        Texture2D enemy;
        public Game1()
        {
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = false;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            enemigos = new List<Enemigo>();
            player = new Player();
            timew = DateTime.Now;
           
            enemy = Content.Load<Texture2D>("img/enemigo");
            player.img = Content.Load<Texture2D>("img/player");
            player.disparoimg = Content.Load<Texture2D>("img/disparo");
            backgroundTexture = Content.Load<Texture2D>("img/fondo");          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tiempoActual = DateTime.Now;
            tiempo = tiempoActual - timew;
            if (tiempo.Seconds == 2)
            {
                enemigos.Add(new Enemigo());
                timew = DateTime.Now;
            }

            KeyboardState emisor = Keyboard.GetState();
            player.Disparar();

            if (player.positionX >= 0)
            {
                if (emisor.IsKeyDown(Keys.A))
                {
                    player.positionX -= 3 ;
                }
            }
            if (player.positionX <= Window.ClientBounds.Width - player.img.Width)
            {
                if (emisor.IsKeyDown(Keys.D))
                {
                    player.positionX += 3;
                }
            }
            if (player.positionY >= 0)
            {
                if (emisor.IsKeyDown(Keys.W))
                {
                    player.positionY -= 3;
                }
            }

            if (player.positionY <= Window.ClientBounds.Height - player.img.Height)
                if (emisor.IsKeyDown(Keys.S))
                {
                    player.positionY += 3;
                }
            // TODO: Add your update logic here
            if (emisor.IsKeyDown(Keys.P))
            {
                    player.visible = true;
            }
           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FloralWhite);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (player.visible == true)
            {
                _spriteBatch.Draw(player.disparoimg, new Rectangle((player.PosBalaX + (player.img.Width/2))-3, player.PosBalaY - 20, player.disparoimg.Width, player.disparoimg.Height), Color.White);
            }
            for (int i = 0; i < enemigos.Count; i++)
            {
                _spriteBatch.Draw(enemy, new Rectangle(0, ((enemigos[i].positionY + i)*enemy.Height)+i , enemy.Width, enemy.Height), Color.White);
            }
            _spriteBatch.Draw(player.img , new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}