using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Cross_the_Road
{
	class Vehicle
	{
		Texture2D vehicle;
		Rectangle screenBounds;
        Rectangle textureBounds;
        bool hit = false;
        bool outsideScreen = false;
        int motion;

        public Vehicle(Texture2D texture, Rectangle screenBounds, int yValue,int trackNumber, int motion)
        {
			this.vehicle=texture;
            this.motion = motion;
			this.screenBounds = screenBounds;
            if(trackNumber==0 || trackNumber==2)
                textureBounds = new Rectangle(-150, yValue, 150, 90);
            else
                textureBounds = new Rectangle(screenBounds.Width, yValue, 150, 90);
		}

		public void Update(Dexter boy){
            
            textureBounds.X += motion;

            if (boy.BoyBounds.Intersects(textureBounds))
            {
                boy.Bottom = true;
                hit = true;
            }
            if ((textureBounds.X < screenBounds.X-400) || (textureBounds.X > screenBounds.Width+400))
				outsideScreen = true;
		}

		public void Draw(SpriteBatch spriteBatch){
			spriteBatch.Draw (vehicle, textureBounds, Color.White);
		}

        public bool IsHit()
        {
            return hit;
        }

        public void setHit(bool hit)
        {
            this.hit = hit;
        }

        public bool IsOutsideScreen()
        {
            return outsideScreen;
        }

        public Rectangle getBounds()
        {
            return textureBounds;
        }
	}
}

