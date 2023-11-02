using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace _1942
{
    public class Game1 : Game
    {      
        //Clase Enemigo
        List<Enemigo> enemigos;
        Texture2D enemy;
        List<Rectangle> enemigoPosisicion = new List<Rectangle>();
        Texture2D nube;
        Texture2D enemy2;


        //Obstaculos
        List<Obstaculo> obstaculos = new List<Obstaculo>();

        //Clase Jugador
        Player player;
        List<Bala> balasJug = new List<Bala>();
        Texture2D avionInicial;
        Texture2D movIz1;
        Texture2D movDe1;
        Texture2D movIz2;
        Texture2D movDe2;

        //Fondo
        Texture2D backgroundTexture;

        //Varibales
        DateTime timew;
        DateTime tiempoActual;
        TimeSpan tiempo;
        DateTime minute;
        DateTime minuteActual;
        TimeSpan minu;
        Random rnd;
        Double game;
        Bala bala;
        Texture2D municion1;
        List<Municion> municion = new List<Municion>();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont fuente;
        int cont = 0, pas = 0, cos=0;
        
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

            //Clase enemigo
            enemigos = new List<Enemigo>();
            enemy = Content.Load<Texture2D>("img/enemigo");
            nube = Content.Load<Texture2D>("img/nube");
            enemy2 = Content.Load<Texture2D>("img/enemigo2");

            //Clase jugador
            player = new Player();
            bala = new Bala();
            avionInicial = Content.Load<Texture2D>("avionInicial");
            player.disparoimg = Content.Load<Texture2D>("img/disparo");
            movIz1 = Content.Load<Texture2D>("movIz1");
            movIz2 = Content.Load<Texture2D>("movIz2");
            movDe1 = Content.Load<Texture2D>("movDe1");
            movDe2 = Content.Load<Texture2D>("movDe2");

            //Otras Variables
            timew = DateTime.Now;
            minute = DateTime.Now;
            rnd = new Random();
            backgroundTexture = Content.Load<Texture2D>("img/fondo");
            fuente = Content.Load<SpriteFont>("img/arial");
            municion1 = Content.Load<Texture2D>("municion1");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            game = gameTime.TotalGameTime.TotalSeconds;
            tiempoActual = DateTime.Now;
            tiempo = tiempoActual - timew;
            minuteActual = DateTime.Now;
            minu = minuteActual - minute;


            //establece municiones cuando el jugador tiene menos de 25 balas o se quedo sin ninguna
            
            if (player.municion < 75 || player.disparar == false)
            {
                if (tiempo.Seconds == 5)
                {   
                    municion.Add(new Municion((rnd.Next(0, (Window.ClientBounds.Width - nube.Height))), rnd.Next(-200, -100), municion1));
                    timew = DateTime.Now;
                }
            }

            //crea nubes y crea enemigos con velocidad 5 cada 5 seg
            if (tiempo.Seconds == 6)
            {
                obstaculos.Add(new Obstaculo(rnd.Next(0, (Window.ClientBounds.Width - nube.Height)), rnd.Next(-150, -100)));
                enemigos.Add(new Enemigo(rnd.Next(0, (Window.ClientBounds.Width - enemy.Height)), rnd.Next(-300, -200), 5, enemy));
                timew = DateTime.Now;
            }  

            //Crea un segundo tipo de enemigo
            if(minu.Seconds == 1)
            {
                enemigos.Add(new Enemigo(rnd.Next(0, (Window.ClientBounds.Width - enemy2.Height)), rnd.Next(-200, -100), 2, enemy2));
                minute = DateTime.Now;
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
                    player.positionX -= 3;
                    player.img = movIz1;
                    if (emisor.IsKeyDown(Keys.A) && pas > 10)
                    {
                        player.img = movIz2;
                    }
                }
            }
            if (emisor.IsKeyUp(Keys.A))
            {
                player.img = avionInicial;
                pas = 0;
            }

            if (player.positionX <= Window.ClientBounds.Width - player.img.Width)
            {
                if (emisor.IsKeyDown(Keys.D))
                {
                    player.positionX += 3;
                    player.img = movDe1;
                    if (emisor.IsKeyDown(Keys.D) && cos > 10)
                    {
                        player.img = movDe2;
                    }
                }
                if (emisor.IsKeyUp(Keys.D))
                {
                    cos = 0;
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
            {
                if (emisor.IsKeyDown(Keys.S))
                {
                    player.positionY += 3;
                }
            }

   
            // TODO: Add your update logic here

            if (player.disparar == true && cont > 3)
            {
                if (emisor.IsKeyDown(Keys.P))
                {
                    balasJug.Add(new Bala());
                    player.municion = player.municion - 2;
                    foreach (Bala b in balasJug)
                    {
                        b.visible = true;
                    }
                }
                cont = 0;
            }
            if(player.municion == 0)
            {
                player.disparar = false;
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
                    if (a.PosBalaY < 0)
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


            for (int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].positionY > Window.ClientBounds.Height)
                {
                    enemigos.RemoveAt(i);
                }
            }
            foreach (Obstaculo o in obstaculos)
            {
                o.mover(nube);
                if(o.positionY > Window.ClientBounds.Height)
                {
                    obstaculos.Remove(o);
                    break;
                }
            }

            if (player.municion < 75 || player.disparar == false)
            {
                foreach (Municion m in municion)
                {
                    m.mover(m.imagen);
                    if (m.positionY > Window.ClientBounds.Height)
                    {
                        municion.Remove(m);
                        break;
                    }
                    if (new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height).Intersects(new Rectangle(m.positionX, m.positionY, m.imagen.Width, m.imagen.Height)))
                    {
                        municion.Remove(m);
                        player.municion = 100;
                        player.disparar = true;
                        municion.Clear();
                        break;
                    }
                }
            }
            cont++;
            cos++;
            pas++;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            foreach (Bala bal in balasJug)
            {
                if (bal.visible == true)
                {
                    _spriteBatch.Draw(player.disparoimg, new Rectangle((bal.PosBalaX + (player.img.Width / 2)) - 10, bal.PosBalaY - 20, player.disparoimg.Width, player.disparoimg.Height), Color.White);
                }
            }
            foreach (Enemigo r in enemigos)
            {
                _spriteBatch.Draw(r.img, new Rectangle(r.positionX, r.positionY, r.img.Width, r.img.Height), Color.White);
            }
            _spriteBatch.DrawString(fuente, "vida: " + player.vida, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(fuente, "score: " + player.score, new Vector2(Window.ClientBounds.Height - player.img.Height, 0), Color.White);
            _spriteBatch.DrawString(fuente, Convert.ToString(game) , new Vector2(Window.ClientBounds.Height/2 , 0), Color.White);
            _spriteBatch.DrawString(fuente, "municion: " + player.municion, new Vector2(0, 24), Color.White);
            if (player.vida >= 50)
            {
                _spriteBatch.Draw(player.img, new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height), Color.White);
            }
            if (player.vida < 50 && player.vida > 10)
            {
                _spriteBatch.Draw(player.img, new Rectangle(player.positionX, player.positionY, player.img.Width, player.img.Height), Color.Red);
            }
            foreach (Obstaculo o in obstaculos)
            {
                _spriteBatch.Draw(nube, new Rectangle(o.positionX, o.positionY, nube.Width, nube.Height), Color.White * 0.7f);
            }
                foreach (Municion m in municion)
                {
                    _spriteBatch.Draw(m.imagen, new Rectangle(m.positionX, m.positionY, m.imagen.Width, m.imagen.Height), Color.White);
                }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}