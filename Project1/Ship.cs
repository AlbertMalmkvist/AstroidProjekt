using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AstroidProjekt
{
    public class Ship
    {
        public Texture2D ShipImg;

        public Vector2 pos;
        public Vector2 velocity;

        public int ScrWidth;
        public int ScrHeight;

        public bool Active;

        public Rectangle popRect;
        public Ship(Texture2D ShipImg, Vector2 pos, Vector2 velocity, int ScrWidth, int ScrHeight)
        {
            this.ShipImg = ShipImg;
            this.pos = pos;
            this.velocity = velocity;
            this.ScrWidth = ScrWidth;
            this.ScrHeight = ScrHeight;

            Active = true;
        }

        public void UpdateShip()
        {
            pos = pos + velocity;

            float othersideX = ShipImg.Width + ShipImg.Width + ScrWidth;
            float othersideY = ShipImg.Height + ShipImg.Height + ScrHeight;

            //Makes it so the Ships appears on the other side of the screen when they leave it, making it so it cant leave the window
            if (pos.X > ScrWidth + ShipImg.Width)
            {
                pos.X = pos.X - othersideX;
            }
            if (pos.X < 0 - ShipImg.Width)
            {
                pos.X = pos.X + othersideX;
            }

            if (pos.Y > ScrHeight + ShipImg.Height)
            {
                pos.Y = pos.Y - othersideY;
            }
            if (pos.Y < 0 - ShipImg.Height)
            {
                pos.Y = pos.Y + othersideY;
            }
            pos = pos + velocity;
            popRect = new Rectangle((int)(pos.X), (int)(pos.Y), ShipImg.Width, ShipImg.Height);
        }

        public void DrawShip(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShipImg, pos, Color.White);
        }

        public bool IsShipDestroyed(int x, int y)
        {
            //Checks to make sure its destroyed and changes Active to false, But it wont make a new ship unline with the astroids
            bool isDestroyed = false;
            Rectangle rect = new Rectangle((int)(pos.X), (int)(pos.Y), ShipImg.Width, ShipImg.Height);

            if (rect.Contains(x, y))
            {
                isDestroyed = true;
                Active = false;
            }

            return isDestroyed;

        }
    }
}
