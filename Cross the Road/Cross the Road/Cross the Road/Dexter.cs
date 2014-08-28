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
	class Dexter
	{
		Texture2D dexter;
		KeyboardState keyboardState;
		Rectangle textureBounds;
		Rectangle screenBounds;
		int motion=1;
		int speed=5;
        bool bottom;
        int score;

		public Dexter(Texture2D texture,Rectangle screenBounds)
		{
            this.score = 0;
            this.bottom = true;
			this.dexter=texture;
			this.screenBounds = screenBounds;
			textureBounds = new Rectangle ((screenBounds.Width - 50)/2, screenBounds.Height - 50,50,50);
		}

		public void Update(){
			keyboardState = Keyboard.GetState ();
			if(keyboardState.IsKeyDown(Keys.Up)){
				motion = -1;
				textureBounds.Y += (motion*speed);
			}
			if(keyboardState.IsKeyDown(Keys.Down)){
				motion = 1;
				textureBounds.Y += (motion*speed);
			}
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                motion = 1;
                textureBounds.X += (motion * speed);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                motion = -1;
                textureBounds.X += (motion * speed);
            }

            ScreenLimit();
            ScoreMethod();
		}

        public void ScoreMethod()
        {
            if (bottom == true && textureBounds.Y < 35)
            {
                bottom = false;
                score += 1;
            }
            else if (bottom == false && textureBounds.Y > 520)
            {
                bottom = true;
                score += 1;
            }
        }

        public void ScreenLimit()
        {
            if (textureBounds.Y < 0)
                textureBounds.Y = 0;
            if (textureBounds.Y + textureBounds.Height > screenBounds.Height)
                textureBounds.Y = screenBounds.Height - textureBounds.Height;
            if (textureBounds.X < 0)
                textureBounds.X = 0;
            if (textureBounds.X + textureBounds.Width > screenBounds.Width)
                textureBounds.X = screenBounds.Width - textureBounds.Width;
        }

		public void Draw(SpriteBatch spriteBatch){
			spriteBatch.Draw (dexter, textureBounds, Color.White);
		}

        public int Score
        {
            set { this.score = value; }
            get { return this.score; }
        }

        public bool Bottom
        {
            set { this.bottom = value; }
            get { return this.bottom; }
        }

        public Rectangle BoyBounds
        {
            get {return textureBounds;}
        }

        public void setInStartPosition()
        {
            textureBounds = new Rectangle((screenBounds.Width - 50) / 2, screenBounds.Height - 50, 50, 50);
        }

        public Vector2 Position
        {
            get { return new Vector2(textureBounds.X, textureBounds.Y); }
        }

        public Rectangle Bounds
        {
            get { return textureBounds; }
        }
	}
}