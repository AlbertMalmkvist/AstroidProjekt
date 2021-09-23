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

        public bool alive;

        public Rectangle popRect;
        public Astroid(Texture2D AstroidImg, Vector2 pos, Vector2 velocity, int ScrWidth, int ScrHeight)
        {
            this.AstroidImg = AstroidImg;
            this.pos = pos;
            this.velocity = velocity;
            this.ScrWidth = ScrWidth;
            this.ScrHeight = ScrHeight;

            alive = true;

            Random rand = new Random();
            int x = rand.Next(0, 1);
        }

        public void UpdateAstroid()
        {
            pos = pos + velocity;

            float othersideX = AstroidImg.Width + AstroidImg.Width + ScrWidth;
            float othersideY = AstroidImg.Height + AstroidImg.Height + ScrHeight;

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
            bool isDestroyed = false;
            Rectangle rect = new Rectangle((int)(pos.X), (int)(pos.Y), AstroidImg.Width, AstroidImg.Height);

            if (rect.Contains(x, y))
            {
                isDestroyed = true;
                alive = false;
            }

            return isDestroyed;

        }
    }
}
