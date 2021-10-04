using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AstroidProjekt
{
    public class Astroid
    {
        public Texture2D AstroidImg;

        public Vector2 pos;
        public Vector2 velocity;

        public int ScrWidth;
        public int ScrHeight;

        public bool Active;

        public Rectangle popRect;

        bool Collide()
        {
            Rectangle AstroidReact = new Rectangle((int)pos.X, (int)pos.Y, AstroidImg.Width, AstroidImg.Height);
            return AstroidReact.Intersects(AstroidReact);
        }
        public Astroid(Texture2D AstroidImg, Vector2 pos, Vector2 velocity, int ScrWidth, int ScrHeight)
        {
            this.AstroidImg = AstroidImg;
            this.pos = pos;
            this.velocity = velocity;
            this.ScrWidth = ScrWidth;
            this.ScrHeight = ScrHeight;

            Active = true;
        }

        public void UpdateAstroid()
        {
                if (Collide())
                {
                    System.Diagnostics.Debug.WriteLine("Hit");
                }
            pos = pos + velocity;

            float othersideX = AstroidImg.Width + AstroidImg.Width +MainGame.ScrWidth;
            float othersideY = AstroidImg.Height + AstroidImg.Height + ScrHeight;

            //Makes it so the astroid appears on the other side of the screen when they leave it, making it so it cant leave the window
            if (pos.X > ScrWidth + AstroidImg.Width)
            {
                pos.X = pos.X - othersideX;
            }
            if (pos.X < 0 - AstroidImg.Width)
            {
                pos.X = pos.X + othersideX;
            }

            if (pos.Y > ScrHeight + AstroidImg.Height)
            {
                pos.Y = pos.Y - othersideY;
            }
            if (pos.Y < 0 - AstroidImg.Height)
            {
                pos.Y = pos.Y + othersideY;
            }
            pos = pos + velocity;

            popRect = new Rectangle((int)(pos.X), (int)(pos.Y), AstroidImg.Width, AstroidImg.Height);
        }

        public void DrawAstroid(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AstroidImg, pos, Color.White);
        }

        public bool IsAstroidDestroyed(int x, int y)
        {

            //Checks to make sure its destroyed and changes Active to false, which make the MainGame make a new astroid
            bool isDestroyed = false;
            Rectangle rect = new Rectangle((int)(pos.X), (int)(pos.Y), AstroidImg.Width, AstroidImg.Height);

            if (rect.Contains(x, y))
            {
                isDestroyed = true;
                Active = false;
            }

            return isDestroyed;

        }
    }
}
