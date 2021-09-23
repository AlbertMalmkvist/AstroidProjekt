using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AstroidProjekt
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D Astroids;
        public Texture2D Ships;

        public Vector2 pos;
        public Vector2 velocity;
        public Vector2 ShipPos;

        public int ScrWidth;
        public int ScrHeight;
        public int score;

        MouseState mouseState, previousMouseState;

        Random rand;

        bool destroyed;

        public Astroid astroid;


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

            Astroids = Content.Load<Texture2D>("astroid");
            Ships = Content.Load<Texture2D>("spacecraft");
            ScrWidth = Window.ClientBounds.Width;
            ScrHeight = Window.ClientBounds.Height;

            mouseState = Mouse.GetState();

            rand = new Random();
            int YDirection = rand.Next(1, 3);
            int XDirection = rand.Next(1, 3);

            int startY = ScrWidth + Astroids.Width;
            int startx = ScrWidth + Astroids.Height;

            Vector2 startpos = new Vector2(startY, startx);
            Vector2 direction = new Vector2(YDirection, XDirection);
            astroid = new Astroid(Astroids, startpos, direction, ScrWidth, ScrHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (pos.X < 0 || pos.X > ScrWidth - Astroids.Width)
            {
                velocity = velocity * -1;
            }

            astroid.UpdateAstroid();
            if (astroid.pos.Y < 0 - Astroids.Height && astroid.alive == true)
            {
                astroid.alive = false;
            }
            else if (astroid.popRect.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    destroyed = astroid.IsAstroidDestroyed(mouseState.X, mouseState.Y);

                    if (destroyed)
                    {
                        score += 10;
                    }
                }
            }

            if (!astroid.alive)
            {
                int YDirection = rand.Next(-3, 3);
                int XDirection = rand.Next(-3, 3);

                int startY = ScrWidth + Astroids.Width;
                int startx = ScrHeight + Astroids.Height;

                Vector2 startpos = new Vector2(startY, startx);
                Vector2 direction = new Vector2(YDirection, XDirection);
                astroid = new Astroid(Astroids, startpos, direction, ScrWidth, ScrHeight);
            }

            System.Diagnostics.Debug.WriteLine("score...." + score);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ShipPos = Vector2.Zero;
            spriteBatch.Begin();
            astroid.DrawAstroid(spriteBatch);
            for(int i = 0; i < 5; i++)
            {
            spriteBatch.Draw(Ships, ShipPos, Color.White);
                ShipPos.X = ShipPos.X + Ships.Width;
            }

            Window.Title = "Astroids " + " Score: " + score;
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
