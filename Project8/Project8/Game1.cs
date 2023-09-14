using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Configuration;
using System.Windows.Forms.VisualStyles;

namespace Project8
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D backgroundTexture;
        Player player;
        bool condicion = false;
        DateTime timew;
        DateTime tiempoActual;
        TimeSpan tiempo;
        int c = 1;
       
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
            player = new Player();
            player.img = Content.Load<Texture2D>("img/player");
            player.disparoimg = Content.Load<Texture2D>("img/disparo");
            backgroundTexture = Content.Load<Texture2D>("img/fondo");          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState emisor = Keyboard.GetState();
            player.Disparar();
            if (emisor.IsKeyDown(Keys.Enter))
            {
                condicion = true;
            }

            if (player.positionX >= 0)
            {
                if (emisor.IsKeyDown(Keys.A))
                {
                    player.positionX -= 3 ;
                }
            }
            if (player.positionX <= 530)
            {
                if (emisor.IsKeyDown(Keys.D))
                {
                    player.positionX += 3;
                }
            }
            if (player.positionX >= 0)
            {
                if (emisor.IsKeyDown(Keys.W))
                {
                    player.positionY -= 3;
                }
            }
            if (player.positionY <= 430)
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (player.visible == true)
            {
                _spriteBatch.Draw(player.disparoimg, new Rectangle((player.PosBalaX + (player.img.Width/2))-3, player.PosBalaY - 20, player.disparoimg.Width, player.disparoimg.Height), Color.White);
            }
            _spriteBatch.Draw(player.img , new Vector2(player.positionX , player.positionY) , Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}