using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AstroidProjekt
{
    public class MainGame : Game
    {
        //All ints used through out the program
        public int ScrWidth;
        public int ScrHeight;
        public int score;
        public int lives = 5;
        public int Timer = 200;

        //Created with the program
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //All Texture2D used through out the program
        public Texture2D Background;
        public Texture2D Astroids;
        public Texture2D Ships;
        public Texture2D Lifes;
        public Texture2D Boom;

        //All Vector2 used through out the program
        public Vector2 pos;
        public Vector2 velocity;
        public Vector2 LifePos;

        // Mousestate
        MouseState mouseState, previousMouseState;

        //Ranomisation
        Random rand;

        bool destroyed;

        //Access to the other classes
        public Astroid astroid;
        public Ship ship;

        //Lists using the other classes
        List<Astroid> AstroidList;
        List<Ship> ShipList;

        Rectangle BoomReact;


        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Getting the images used
            Astroids = Content.Load<Texture2D>("astroid");
            Ships = Content.Load<Texture2D>("spacecraft");
            Lifes = Content.Load<Texture2D>("Lifes");
            Background = Content.Load<Texture2D>("spaceFront");
            Boom = Content.Load<Texture2D>("explosion");

            ScrWidth = Window.ClientBounds.Width;
            ScrHeight = Window.ClientBounds.Height;

            mouseState = Mouse.GetState();

            AstroidList = new List<Astroid>();
            ShipList = new List<Ship>();

            rand = new Random();

            for (int a = 0; a <= 2; a++){
            int YDirection = rand.Next(a + 1, a + 1);
            int XDirection = rand.Next(a + 1, a + 1);

            int startY = ScrWidth + Astroids.Width;
            int startx = ScrWidth + Astroids.Height;

            Vector2 startpos = new Vector2(startY, startx);
            Vector2 direction = new Vector2(YDirection, XDirection);
            astroid = new Astroid(Astroids, startpos, direction, ScrWidth, ScrHeight);

            AstroidList.Add(astroid);
            }

            for (int b = 0; b <= 5; b++){
                int startY = ScrWidth + Ships.Width;
                int startx = ScrWidth + Ships.Height;
                if(b == 0){
                Vector2 direction = new Vector2(1, 3);
                Vector2 startpos = new Vector2(startY, startx);
                ship = new Ship(Ships, startpos, direction, ScrWidth, ScrHeight);
                ShipList.Add(ship);
                }
                else if (b == 1)
                {
                    Vector2 direction = new Vector2(3, -1);
                    Vector2 startpos = new Vector2(startY, startx);
                    ship = new Ship(Ships, startpos, direction, ScrWidth, ScrHeight);
                    ShipList.Add(ship);
                }
                else if (b == 2)
                {
                    Vector2 direction = new Vector2(1, -3);
                    Vector2 startpos = new Vector2(startY, startx);
                    ship = new Ship(Ships, startpos, direction, ScrWidth, ScrHeight);
                    ShipList.Add(ship);
                }
                else if (b == 3)
                {
                    Vector2 direction = new Vector2(-3, -1);
                    Vector2 startpos = new Vector2(startY, startx);
                    ship = new Ship(Ships, startpos, direction, ScrWidth, ScrHeight);
                    ShipList.Add(ship);
                }
                else
                {
                    Vector2 direction = new Vector2(3, 1);
                    Vector2 startpos = new Vector2(startY, startx);
                    ship = new Ship(Ships, startpos, direction, ScrWidth, ScrHeight);
                    ShipList.Add(ship);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            BoomReact = new Rectangle(mouseState.X - Boom.Width / 2, mouseState.Y - Boom.Height / 2, Boom.Width, Boom.Height);

            if (pos.X < 0 || pos.X > ScrWidth - Astroids.Width)
            {
                velocity = velocity * -1;
            }

            if (lives > 0)
            {
                foreach (Astroid astroid in AstroidList)
                {
                    astroid.UpdateAstroid();
                    if (astroid.pos.Y < 0 - Astroids.Height && astroid.Active == true)
                    {
                        astroid.Active = false;
                    }
                    else if (astroid.popRect.Contains(mouseState.X, mouseState.Y))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            destroyed = astroid.IsAstroidDestroyed(mouseState.X, mouseState.Y);

                            if (destroyed)
                            {
                                score += 10;
                                Timer = 0;
                                if (score == 1000)
                                {
                                    AstroidList.Clear();
                                    ShipList.Clear();
                                }
                            }
                        }
                    }
                    if (!astroid.Active)
                    {
                        AstroidList.Remove(astroid);

                        int YDirection = rand.Next(-3, 3);
                        int XDirection = rand.Next(-3, 3);
                        if (YDirection == 0 || XDirection == 0)
                        {
                            XDirection = 4;
                            YDirection = -4;
                        }

                        int startY = ScrWidth + Astroids.Width;
                        int startx = ScrHeight + Astroids.Height;

                        Vector2 startpos = new Vector2(startY, startx);
                        Vector2 direction = new Vector2(YDirection, XDirection);
                        Astroid Newastroid = new Astroid(Astroids, startpos, direction, ScrWidth, ScrHeight);

                        AstroidList.Add(Newastroid);

                        break;
                    }
                }

                foreach (Ship ship in ShipList)
                {
                    {
                        ship.UpdateShip();
                        if (ship.pos.Y < 0 - Ships.Height && ship.Active == true)
                        {
                            ship.Active = false;
                        }
                        else if (ship.popRect.Contains(mouseState.X, mouseState.Y))
                        {
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                destroyed = ship.IsShipDestroyed(mouseState.X, mouseState.Y);

                                if (destroyed)
                                {
                                    lives--;
                                    Timer = 0;
                                }
                            }
                        }
                        if (!ship.Active)
                        {
                            ShipList.Remove(ship);
                            break;
                        }
                    }
                }

            }
            else if (lives == 0)
            {
                AstroidList.Clear();
                ShipList.Clear();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            LifePos = Vector2.Zero;
            spriteBatch.Begin();

            spriteBatch.Draw(Background, Vector2.Zero, Color.White);

            foreach(Astroid astroid in AstroidList){
            astroid.DrawAstroid(spriteBatch);
            }
            foreach(Ship ship in ShipList)
            {
                ship.DrawShip(spriteBatch);
                spriteBatch.Draw(Lifes, LifePos, Color.White);
                LifePos.X = LifePos.X + Lifes.Width;
            }
            if (Timer < 200)
            {
                Timer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                spriteBatch.Draw(Boom, BoomReact, Color.White);
            }
            Window.Title = "Astroids " + " Score: " + score + "   Lives:" + lives;
            if (score == 1000)
            {
                Window.Title = "Astroids " + "      YOU WIN";
            }
            if (lives == 0)
            {
                Window.Title = "Astroids " + "      YOU LOSE";
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
