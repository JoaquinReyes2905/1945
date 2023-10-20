using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Principal;

namespace Project8
{
    public class Game1 : Game
    {

        //Clase Enemigo
        List<Enemigo> enemigos;
        Texture2D enemy;
        List<Rectangle> enemigoPosisicion = new List<Rectangle>();
        Texture2D nube;
        Texture2D enemy2;
        Texture2D enemy3;


        //Obstaculos
        List<Obstaculo> obstaculos = new List<Obstaculo>(); 

        //Clase Jugador
        Player player;
        List<Bala> balasJug = new List<Bala>();
        //Fondo
        Texture2D backgroundTexture;

        //Varibales
        DateTime timew;
        DateTime tiempoActual;
        TimeSpan tiempo;
        Random rnd;
        Bala bala;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont fuente;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
             Content.RootDirectory = "Content";
             IsMouseVisible = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferWidth = 600;
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
            
            //Clase Enemigos
            enemigos = new List<Enemigo>();
            enemy = Content.Load<Texture2D>("img/enemigo");
            nube = Content.Load<Texture2D>("img/nube");
            enemy2 = Content.Load<Texture2D>("img/enemigo2");
            enemy3 = Content.Load<Texture2D>("img/enemigo3");
            
            //Clase jugador
            player = new Player();
            bala = new Bala();
            player.img = Content.Load<Texture2D>("img/player");
            player.disparoimg = Content.Load<Texture2D>("img/disparo");
            
            //Otras Variables
            timew = DateTime.Now;
            rnd = new Random();
            backgroundTexture = Content.Load<Texture2D>("img/fondo");
            fuente = Content.Load<SpriteFont>("img/arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tiempoActual = DateTime.Now;
            tiempo = tiempoActual - timew;

            if (tiempo.Milliseconds == 700)
            {
                enemigos.Add(new Enemigo(rnd.Next(0, (Window.ClientBounds.Width - enemy.Height)), rnd.Next(-300, -200), 5, enemy));
                timew = DateTime.Now;
            }
            if (tiempo.Milliseconds == 600)
            {
                obstaculos.Add(new Obstaculo(rnd.Next(0, (Window.ClientBounds.Width - nube.Height)), rnd.Next(-200, -100)));
                timew = DateTime.Now;

            }
            if (tiempo.Seconds == 1)
            {
                enemigos.Add(new Enemigo(rnd.Next(0, (Window.ClientBounds.Width - enemy2.Height)), rnd.Next(-300, -200), 2 , enemy2));
                timew = DateTime.Now;
            }
           
            if (player.vida == 0)
            {
                Exit();
            }
            KeyboardState emisor = Keyboard.GetState();
            foreach (Bala b in balasJug)
            {
                b.Disparar(player);
            }

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
                balasJug.Add(new Bala());
                foreach(Bala b in balasJug)
                {
                    b.visible = true;
                }
            }

            //Colisiones
            foreach (Enemigo r in enemigos)
            {
                r.mover(enemy);
                enemigoPosisicion.Add(new Rectangle(r.positionX, r.positionY, enemy.Width, enemy.Height));
                foreach (Bala a in balasJug)
                {
                    if (a.visible == true)
                    {
                        if (new Rectangle(r.positionX, r.positionY, r.img.Width, r.img.Height).Intersects(new Rectangle((a.PosBalaX + (player.img.Width / 2)) - 3, a.PosBalaY - 20, player.disparoimg.Width, player.disparoimg.Height)))
                        {
                            balasJug.Remove(a);
                            enemigos.Remove(r);
                            player.score = player.score + 20;
                            break;
                            
                        }
                    }
                    if(a.PosBalaY < 0)
                    {
                        balasJug.Remove(a);
                        break;
                    }
                }
                if (new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height).Intersects(new Rectangle(r.positionX, r.positionY, r.img.Width, r.img.Height)))
                {
                    enemigos.Remove(r);
                    player.vida = player.vida - 25;
                    break;
                }
                break;
            }
           //Fin de colisiones


            for(int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].positionY > Window.ClientBounds.Height)
                {
                    enemigos.RemoveAt(i);
                }
            }
            foreach (Obstaculo o in obstaculos)
            {
                o.mover(nube);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture , new Rectangle( 0 , 0 , Window.ClientBounds.Width , Window.ClientBounds.Height) , Color.White);
            
            foreach (Bala bal in balasJug)
            {
                if (bal.visible == true)
                {
                    _spriteBatch.Draw(player.disparoimg, new Rectangle((bal.PosBalaX + (player.img.Width / 2)) - 3, bal.PosBalaY - 20, player.disparoimg.Width, player.disparoimg.Height), Color.White);
                }
            }
            foreach (Enemigo r in enemigos)
            {    
                    _spriteBatch.Draw(r.img, new Rectangle(r.positionX, r.positionY, r.img.Width, r.img.Height), Color.White);
            }
            _spriteBatch.DrawString(fuente , "vida: " + player.vida , new Vector2(0,0) , Color.White);
            _spriteBatch.DrawString(fuente, "score: " + player.score, new Vector2(Window.ClientBounds.Height - player.img.Height, 0), Color.White);
            if (player.vida >= 50)
            {
                _spriteBatch.Draw(player.img, new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height), Color.White);
            }
            if(player.vida < 50 && player.vida > 10) 
            {
                _spriteBatch.Draw(player.img, new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height), Color.Red);
            }
            foreach (Obstaculo o in obstaculos)
            {
                _spriteBatch.Draw(nube, new Rectangle(o.positionX,o.positionY,nube.Width,nube.Height),Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}